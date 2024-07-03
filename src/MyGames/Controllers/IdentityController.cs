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
using MyGames.Dtos;

#pragma warning disable CS8602
#pragma warning disable CS8604

namespace MyGames.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IUserRoleService userRoleService;
        private readonly IValidator<LoginDto> loginValidator;
        private readonly IValidator<RegistrationDto> registrationValidator;

        public IdentityController(IUserService userService, IValidator<LoginDto> loginValidator, 
            IValidator<RegistrationDto> registrationValidator, IUserRoleService userRoleService, IRoleService roleService)
        {
            this.userService = userService;
            this.loginValidator = loginValidator;
            this.registrationValidator = registrationValidator;
            this.userRoleService = userRoleService;
            this.roleService = roleService;
        }


        [HttpGet("/[controller]/[action]", Name = "LoginView")]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return base.View();
        }

        [HttpPost("/api/[controller]/[action]", Name = "LoginEndpoint")]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            try 
            {
                loginDto.ReturnUrl = ViewBag.ReturnUrl as string;

                var validationResult = await loginValidator.ValidateAsync(loginDto);

                if (!validationResult.IsValid) {
                    foreach(var error in validationResult.Errors)
                    {
                        base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                        
                    return base.View();
                }

                var foundUser = await userService.Login(loginDto);

                if (foundUser == null || foundUser.Id == null)
                    {
                        base.TempData["error"] = "Incorrect login or password!";
                        System.Console.WriteLine(base.TempData["error"] as string);
                        return base.RedirectToRoute("LoginView", new
                        {
                            ReturnUrl = loginDto.ReturnUrl,
                        });
                    }

                var claims = new List<Claim> {
                    new(ClaimTypes.Email, foundUser.Email!),
                        new("login", foundUser.Login!),
                        new("id", foundUser.Id.ToString()!),
                        new("username", foundUser.Username!),
                };

                var roles = await userRoleService.GetAllRolesByUserId((int)foundUser.Id);

                foreach(var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                if (string.IsNullOrWhiteSpace(loginDto.ReturnUrl) == false)
                {
                    return base.Redirect(loginDto.ReturnUrl);
                }


                return base.RedirectToAction(controllerName: "Home", actionName: "Index");
            }
            catch
            {
                base.TempData["error"] = "Incorrect login or password!";

                return base.RedirectToRoute("LoginView", new
                {
                    loginDto.ReturnUrl
                });
            }
        }

        [HttpGet("/[controller]/[action]", Name = "RegistrationView")]
        public IActionResult Registration()
        {
            return base.View();
        }

        [HttpPost("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
        public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto)
        {
            try
            {
                var validationResult = await registrationValidator.ValidateAsync(registrationDto);

                if (!validationResult.IsValid) {
                    foreach(var error in validationResult.Errors)
                        base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                    return base.View();
                }

                await userService.CreateAsync(registrationDto);
                var registratedUser = await userService.Login(new LoginDto() 
                    { 
                        Login = registrationDto.Login, 
                        Password = registrationDto.Password
                    }
                );

                var role = await roleService.GetByNameAsync("User");

                if(registratedUser is null)
                {
                    throw new Exception("could't register");
                }

                await userRoleService.CreateAsync(new UserRole(){
                    UserId = registratedUser.Id, 
                    RoleId = role.Id,
                });

                return base.RedirectToRoute("LoginView");
            }
            catch (Exception ex)
            {
                base.TempData["error"] = ex.Message;
                return base.View();
            }
        }

        [HttpGet("/api/[controller]/[action]")]
        public async Task<IActionResult> Logout(string? ReturnUrl)
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return base.RedirectToRoute(routeName: "LoginView", routeValues: new
            {
                ReturnUrl
            });
        }

        [HttpGet("/api/[controller]/[action]/{id}")]
        public async Task<IActionResult> Avatar(int id) 
        { 
            var foundUser = await userService.GetByIdAsync(id);
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