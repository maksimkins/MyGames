using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyGames.Models;
using MyGames.Services.Base;

namespace MyGames.Controllers
{
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

        [HttpGet("{gameid}")]
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

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody]Comment comment) 
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
                await service.CreateCommentAsync(comment);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteComment(int Id)
        {
            try
            {
                var comment = new Comment()
                {
                    Id = Id
                };

                await service.DeleteCommentAsync(comment);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put([FromBody]Comment comment, int Id)
        {
            // try
            // {
                var result = await validator.ValidateAsync(comment);

                if(!result.IsValid)
                {
                    foreach(var error in result.Errors)
                    {
                        base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest();
                }
                
                await service.ChangeCommentAsync(Id, comment);

                return Ok();
            // }
            // catch(Exception ex)
            // {
            //     return StatusCode(500, ex.Message);
            // }
        }
    }
}