using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyGames.Controllers;

using MyGames.Core.User.Models;
using MyGames.Core.Comment.Models;
using MyGames.Core.Role.Models;

using MyGames.Core.Comment.Services.Base;

[Authorize("MyPolicy")]
[Route("[controller]")]
public class CommentController : Controller
{
    private readonly ICommentService service;
    private readonly IValidator<Comment> validator;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<Role> roleManager;
    public CommentController(ICommentService service, IValidator<Comment> validator, 
        SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager) 
    {
        this.service = service;
        this.validator = validator;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    [AllowAnonymous]
    [HttpGet("/api/[controller]/{gameid}")]
    public async Task<IActionResult> GetComments(int gameId) 
    {
        try
        {
            ViewBag.gameId = gameId;
            var comments = await service.GetCommentsByGameAsync(gameId);
            return View(comments);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("/api/[controller]")] 
    public async Task<IActionResult> CreateComment([FromBody]Comment comment) 
    {
        try
        {  
            var result = await validator.ValidateAsync(comment);

            if(!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    base.ModelState.AddModelError("All", error.ErrorMessage);
                }
                return BadRequest();
            }
            await service.CreateCommentAsync(comment);

            return Ok();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("/api/[controller]/{Id}")]
    public async Task<IActionResult> DeleteComment(int Id)
    {
        try
        {
            var comment = new Comment()
            {
                Id = Id
            };

            var userName =  base.User.FindFirstValue(ClaimTypes.Name);
            var user = await userManager.FindByNameAsync(userName!);
            var userId = user?.Id ?? 0;
            
            await service.DeleteCommentAsync(userId, comment);
            return Ok();
        }
        catch(ArgumentOutOfRangeException ex)
        {
            return BadRequest(error: ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("/api/[controller]/{Id}")]
    public async Task<IActionResult> Put([FromBody]Comment comment, int Id)
    {
        try
        {
            var result = await validator.ValidateAsync(comment);

            if(!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest();
            }
            
            var userName =  base.User.FindFirstValue(ClaimTypes.Name);
            var user = await userManager.FindByNameAsync(userName!);
            var userId = user?.Id ?? 0;

            await service.ChangeCommentAsync(userId, Id, comment);

            return Ok();
        }
        catch(ArgumentOutOfRangeException ex)
        {
            return BadRequest(error: ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
