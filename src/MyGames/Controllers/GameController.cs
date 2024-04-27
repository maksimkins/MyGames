using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyGames.Models;
using System.Text.Json;
using MyGames.Services.Base;

namespace MyGames.Controllers;

public class GameController : Controller {

    private readonly IGameService service;

    public GameController(IGameService service)
    {
        this.service = service;
    }

    
    [HttpGet("[controller]")]
    public async Task<IActionResult> Index ()
    {
        var games = await service.AllGamesAsync();
        return View(games);
    }
}