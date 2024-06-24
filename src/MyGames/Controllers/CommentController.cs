using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGames.Models;
using MyGames.Services.Base;

namespace MyGames.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService service;
        private readonly IValidator<Comment> validator;
        public CommentController(ICommentService service, IValidator<Comment> validator) 
        {
            this.service = service;
            this.validator = validator;
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
            System.Console.WriteLine(comment.UserId);
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

                Int32.TryParse(base.User.Claims.Where(c => c.Type == "id").FirstOrDefault()?.Value, out int userId);
                await service.DeleteCommentAsync(userId, comment);
                return Ok();
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest(error: ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/[controller]/{Id}")]
        public async Task<IActionResult> Put([FromBody]Comment comment, int Id)
        {
            try
            {
                System.Console.WriteLine($"{comment.Text}");
                var result = await validator.ValidateAsync(comment);

                if(!result.IsValid)
                {
                    foreach(var error in result.Errors)
                    {
                        base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest();
                }
                
                Int32.TryParse(base.User.Claims.Where(c => c.Type == "id").FirstOrDefault()?.Value, out int userId);
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
}