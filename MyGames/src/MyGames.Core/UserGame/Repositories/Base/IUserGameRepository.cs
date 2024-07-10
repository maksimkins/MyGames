namespace MyGames.Core.UserGame.Repositories.Base;

using MyGames.Core.UserGame.Models;
using MyGames.Core.Game.Models;
using MyGames.Core.Base.Methods;

public interface IUserGameRepository : ICreateAsync<UserGame>
{
    public Task<IEnumerable<Game?>> GetAllUsersGames(int userId);
    public Task<bool> HasUserGame(int gameId, int userId);
}
