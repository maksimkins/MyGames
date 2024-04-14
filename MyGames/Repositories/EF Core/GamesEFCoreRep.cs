using MyGames.Repositories.Base;
using MyGames.Models;

namespace MyGames.Repositories.EF_Core;

public class GamesEFCoreRep : IGameRepository {
    private readonly MyGamesDbContext dbContext;

    public GamesEFCoreRep() {
        this.dbContext = new MyGamesDbContext();
    }

    public IQueryable<Game> GetAll() {
        return dbContext.Games;
    }
    public Game GetById(int id) {
        return dbContext.Games.FirstOrDefault(g => g.Id == id) ?? 
            throw new ArgumentException("there is no such game!");
    }
    public async void Post(Game game){
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
    }
    // public void Update(int id, Game game);
    public async void Delete(int id) {
        Game game = GetById(id);
        dbContext.Remove<Game>(game);
        await dbContext.SaveChangesAsync();
    }
    // public IQueryable<Game> GetAllByUserId(int id) {
    //     // return dbContext.Games.Where(g => g.UserId == id);
    // }

}