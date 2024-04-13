using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyGames.Models;
using System.Text.Json;

namespace MyGames.Controllers;

public class GameController : Controller {

    private readonly ILogger<HomeController> _logger;

    public GameController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    
    [Route("[controller]")]
    [ActionName("Index")]
    [HttpGet]
    public IActionResult Index ()
    {
        // System.Console.WriteLine("index");
        return View();
    }

    
    [Route("[controller]")]
    [HttpPost]
    public async Task<IActionResult> CreateGame(GameDto newGame) {

        if(newGame.Name is null || string.IsNullOrWhiteSpace(newGame.Name) || string.IsNullOrEmpty(newGame.Name) || newGame.Price is null) {
            return BadRequest();
        }
 
        var gamesJSon = await System.IO.File.ReadAllTextAsync("../../src/MyGames/json/Games.json");

        var games = JsonSerializer.Deserialize<List<Game>>(gamesJSon, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
        });

        games?.Add(new Game() {
            Name = newGame.Name,
            Price = newGame.Price,
        });

        var resultGamesJson = JsonSerializer.Serialize(games, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
        });

        await System.IO.File.WriteAllTextAsync("../../src/MyGames/json/Games.json", resultGamesJson);

        return base.RedirectToAction(actionName: "Index");
    }
}