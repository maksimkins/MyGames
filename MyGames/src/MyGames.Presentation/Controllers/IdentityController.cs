#pragma warning disable CS8602
#pragma warning disable CS8604
#pragma warning disable CS1998

namespace MyGames.Controllers;

using MyGames.Core.Dtos.Models;
using MyGames.Core.User.Models;
using MyGames.Core.Role.Models;

using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

public class IdentityController : Controller
{
    private readonly IValidator<LoginDto> loginValidator;
    private readonly IValidator<RegistrationDto> registrationValidator;
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;

    public IdentityController(IValidator<LoginDto> loginValidator, IValidator<RegistrationDto> registrationValidator,
        SignInManager<User> signInManager, UserManager<User> userManager,RoleManager<Role> roleManager)
    {
        this.loginValidator = loginValidator;
        this.registrationValidator = registrationValidator;
        this.roleManager = roleManager;
        this.signInManager = signInManager;
        this.userManager = userManager;
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
        loginDto.ReturnUrl = ViewBag.ReturnUrl as string;

        var validationResult = await loginValidator.ValidateAsync(loginDto);

        if (!validationResult.IsValid) 
        {
            foreach(var error in validationResult.Errors)
            {
                base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
    
            return base.View();
        }       

        var user = await this.userManager.FindByNameAsync(loginDto.Username);

        if(user == null) 
        {
            System.Console.WriteLine($"{loginDto.Password}");
            base.TempData["error"] = "Incorrect login or password!";
            return base.RedirectToRoute("LoginView", new
            {
                ReturnUrl = loginDto.ReturnUrl,
            });
        }
        
        var result = await this.signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);   

        if(result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");

            return string.IsNullOrWhiteSpace(loginDto.ReturnUrl) == false 
                ? base.Redirect(loginDto.ReturnUrl) 
                : base.RedirectToAction(controllerName: "Home", actionName: "Index");
        }
            
        base.TempData["error"] = "Incorrect login or password!";

        return base.RedirectToRoute("LoginView", new
        {
            loginDto.ReturnUrl
        });
        
    
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
                {
                    base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return base.View();
            }
            var user = new User() {
                    Email = registrationDto.Email,
                    UserName = registrationDto.Username,
                    Birthdate = registrationDto.Birthdate,
                };
            var result = await userManager.CreateAsync(user, registrationDto.Password);
            if(!result.Succeeded)
            {
                throw new Exception();
            }
            if(!await roleManager.RoleExistsAsync("User"))
                    await roleManager.CreateAsync(new Role()
                    {
                        Name = "User",
                    });
            await userManager.AddToRoleAsync(user, "User");
            return base.RedirectToRoute("LoginView");
        }
        catch(Exception)
        {
            base.TempData["error"] = "could't register";
            return base.View();
        }
    }

    [HttpGet("/api/[controller]/[action]")]
    public async Task<IActionResult> Logout(string? ReturnUrl)
    {
        await this.signInManager.SignOutAsync();

        return base.RedirectToRoute(routeName: "LoginView", routeValues: new
        {
            ReturnUrl
        });
    }

    [Authorize]
    [HttpGet("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> Avatar(int id) 
    { 
        string? path = null;

        if (path == null)
        {
            path = "Assets/Avatars/Default.jpg";
        }
        var fileStream = System.IO.File.Open(path, FileMode.Open);
        return base.File(fileStream, "profile_pic/jpeg");
    }
}
