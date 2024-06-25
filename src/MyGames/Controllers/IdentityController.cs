using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyGames.Models;
using MyGames.Services.Base;
using FluentValidation;
using System.Text.Json;

namespace MyGames.Controllers
{
     public class IdentityController : Controller
    {
        private readonly IDataProtector dataProtector;
        private readonly IUserService service;

        private readonly IValidator<User> validator;
        public IdentityController(IUserService service, IDataProtectionProvider dataProtectionProvider, IValidator<User> validator)
        {
            this.validator = validator;
            this.service = service;
            this.dataProtector = dataProtectionProvider.CreateProtector("identity");
        }

        [HttpGet("/[controller]/[action]", Name = "LoginView")]
        public IActionResult Login(string? ReturnUrl)
        {
            var errorMessage = base.TempData["error"];

            ViewBag.ReturnUrl = ReturnUrl;

            if (errorMessage != null)
            {
                base.ModelState.AddModelError("All", errorMessage.ToString()!);
            }


            return base.View();
        }

        [HttpPost("/api/[controller]/[action]", Name = "LoginEndpoint")]
        public async Task<IActionResult> Login([FromForm] User userToLogin)
        {
            try
            {
                var foundUser = await service.Login(userToLogin);

                if (foundUser == null)
                {
                    base.TempData["error"] = "Incorrect login or password!";
                    return base.RedirectToRoute("LoginView", new
                    {
                        ReturnUrl = "/Home/Index"
                    });
                }

                var claims = new Claim[] {
                    new(ClaimTypes.Email, foundUser.Email!),
                    new("login", foundUser.Login!),
                    new("id", foundUser.Id.ToString()!),
                    new("username", foundUser.Username!),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return base.RedirectToAction(controllerName: "Home", actionName: "Index");
            }

            catch (Exception ex)
            {
                base.TempData["error"] = ex.Message;
                return base.RedirectToRoute("LoginView", new
                {
                    ReturnUrl = "/Home/Index"
                });
            }

            
        }

        [HttpGet("/[controller]/[action]", Name = "RegistrationView")]
        public IActionResult Registration()
        {
            if (TempData["error"] != null)
            {
                ModelState.AddModelError("All", "Something went wrong. Please try again");
            }

            return base.View();
        }

        [HttpPost("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
        public async Task<IActionResult> Registration([FromForm] User userToRegister, IFormFile avatar)
        {
            var result = await validator.ValidateAsync(userToRegister);

            if(!result.IsValid)
            {
                base.TempData["error"] = "fail to register";
                return base.RedirectToRoute("RegistrationView");
            }

            try
            {
                if(avatar is not null)
                {
                    var extension = new FileInfo(avatar.FileName).Extension[1..];
                    userToRegister.AvatarUrl = $"Assets/Avatars/{userToRegister.Username}.{extension}";


                    using var newFileStream = System.IO.File.Create(userToRegister.AvatarUrl);
                    await avatar.CopyToAsync(newFileStream);
                }
 
                await service.CreateAsync(userToRegister);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return base.RedirectToRoute("RegistrationView");
            }

            return base.RedirectToRoute("LoginView");
        }

        [HttpGet("/api/[controller]/[action]")]
        public async Task<IActionResult> Logout(string? ReturnUrl)
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return base.RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpGet("/api/[controller]/[action]/{id}")]
        public async Task<IActionResult> Avatar(int id) {

            var foundUser = await service.GetByIdAsync(id);
            string? path = foundUser?.AvatarUrl;

            if (foundUser == null || path == null)
            {
                path = "Assets/Avatars/Default.jpg";
            }
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            return base.File(fileStream, "profile_pic/jpeg");
        }
    }
}