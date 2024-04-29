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

        [HttpGet("{gameid?}")]
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
        public async Task<IActionResult> CreateComment(Comment comment) {
            try
            {   
                service.CreateCommentAsync(comment);
                return Redirect($"/Game/{comment.GameId}");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Comment comment = new Comment() { Id = id };
                service.DeleteCommentAsync(comment);
                return Redirect("/Game");

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Put(Comment comment)
        {
            try
            {
                int gameId = comment.Id is null ? 0 : (int)comment.Id;
                service.ChangeCommentAsync(gameId, comment);
                return Redirect($"/Game/{comment.GameId}");

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}