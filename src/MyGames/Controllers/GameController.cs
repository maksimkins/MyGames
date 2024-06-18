using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyGames.Models;
using System.Text.Json;
using MyGames.Services.Base;

namespace MyGames.Controllers;
[Route("[controller]")]
public class GameController : Controller {

    private readonly IGameService service;

    public GameController(IGameService service)
    {
        this.service = service;
    }
    
    [HttpGet]
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

    [HttpGet("{gameId}")]
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
}