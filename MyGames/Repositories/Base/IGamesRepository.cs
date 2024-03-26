
namespace MyGames.Repositories.Base;

using MyGames.Models;

public interface IGameRepository {
    public IQueryable<Game> GetAll();
    public Game GetById(int id);
    public void Post(Game game);
    // public void Update(int id, Game game);
    public void Delete(int id);
    public IQueryable<Game> GetAllByUserId(int id);
}