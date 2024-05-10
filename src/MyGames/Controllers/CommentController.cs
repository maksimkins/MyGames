using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyGames.Models;
using MyGames.Services.Base;

namespace MyGames.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService service;
        public CommentController(ICommentService service) {
            this.service = service;
        }

        [HttpGet("{gameid}")]
        public async Task<IActionResult> GetComments(int gameId) {
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
        public IActionResult CreateComment(Comment comment) {
            try
            {   
                service.CreateCommentAsync(comment);
                return Redirect($"Comment/{comment.GameId}");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteComment([FromBody]Comment comment)
        {
            try
            {
                service.DeleteCommentAsync(comment);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]Comment comment)
        {
            try
            {
                int gameId = comment.Id is null ? 0 : (int)comment.Id;
                service.ChangeCommentAsync(gameId, comment);
                return Ok();

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}