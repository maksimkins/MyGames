using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MyGames.Controllers;

using MyGames.Core.Game.Models;
using MyGames.Core.Game.Services.Base;

[Route("[controller]")]
public class GameController : Controller {

    private readonly IGameService service;

    public GameController(IGameService service)
    {
        this.service = service;
    }
    
    [HttpGet("/[controller]")]
    public IActionResult Index ()
    {
        return View(); 
    }

    [HttpGet("/[controller]/{gameId}")]
    public async Task<IActionResult> GameInfo(int gameId) 
    {
        try
        {
            var game = await service.GameByIdAsync(gameId);
            return View(game);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize(Roles = "Developer")]
    [HttpGet("/[controller]/[action]", Name = "DeleteGameView")]
    public async Task<IActionResult> Delete ()
    {
        try
        {
            var games = await service.AllGamesAsync();
            return View(games);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }  
    }

    [Authorize(Roles = "Developer")]
    [HttpDelete("/api/[controller]/{Id}")]
        public async Task<IActionResult> DeleteGame(int Id)
        {
            try
            {
                var game = new Game()
                {
                    Id = Id
                };

                await service.DeleteGameAsync(game);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
}