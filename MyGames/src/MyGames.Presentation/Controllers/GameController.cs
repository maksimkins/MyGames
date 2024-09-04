using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MyGames.Controllers;

using MyGames.Core.Game.Models;
using MyGames.Core.Game.Services.Base;
using MyGames.Core.User.Models;
using MyGames.Core.UserGame.Services.Base;

[Route("[controller]")]
public class GameController : Controller {

    private readonly IGameService service;
    private readonly IUserGameService userGameService; 

    public GameController(IGameService service, IUserGameService userGameService)
    {
        this.service = service;
        this.userGameService = userGameService;
    }
    
    [HttpGet("/[controller]")]
    public async Task<IActionResult> Index ()
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

    [Authorize]
    [HttpGet("/[controller]/[action]/{userId}")]
    public async Task<IActionResult> Library (int userId)
    {
        try
        {
            var games = await service.GetAllFromUserLibraryAsync(new User(){
                Id = userId
            });
            
            return View(games);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
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

    [Authorize]
    [HttpPost("/api/[controller]/[action]/{Id}")]
    public async Task<IActionResult> BuyGame(int Id, [FromQuery]int userId)
    {
        try
        {
            var hasGame = await userGameService.HasUserGame(new User{
                Id = userId
            }, new Game{
                Id = Id
            });

            if(!hasGame)
            {
                await userGameService.BuyAsync(new User{
                Id = userId
                }, new Game{
                    Id = Id 
                });
            }


            return Ok();
       }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}