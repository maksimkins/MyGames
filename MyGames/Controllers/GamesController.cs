namespace MyGames.Controllers;

using MyGames.Repositories.EF_Core;

public class GamesController {
    private readonly GamesEFCoreRep rep;

    public GamesController() {
        rep = new GamesEFCoreRep();
    }
}