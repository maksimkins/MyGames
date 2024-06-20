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

        [Route("/[controller]/[action]", Name = "LoginView")]
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

        [HttpPost]
        [Route("/api/[controller]/[action]", Name = "LoginEndpoint")]
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

        [Route("/[controller]/[action]", Name = "RegistrationView")]
        public IActionResult Registration()
        {
            if (TempData["error"] != null)
            {
                ModelState.AddModelError("All", "Something went wrong. Please try again");
            }

            return base.View();
        }

        [HttpPost]
        [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
        public async Task<IActionResult> Registration([FromForm] User userToRegister)
        {
            var result = await validator.ValidateAsync(userToRegister);

            if(!result.IsValid)
            {
                base.TempData["error"] = "fail to register";
                return base.RedirectToRoute("RegistrationView");
            }

            try
            {
                await service.CreateAsync(userToRegister);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return base.RedirectToRoute("RegistrationView");
            }

            return base.RedirectToRoute("LoginView");
        }

        [HttpGet]
        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> Logout(string? ReturnUrl)
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return base.RedirectToRoute("LoginView", new
            {
                ReturnUrl
            });
        }
    }
}