namespace MyGames.Core.UserGame.Services.Base;

using MyGames.Core.User.Models;
using MyGames.Core.Game.Models;

public interface IUserGameService
{
    public Task BuyAsync(User? user, Game? game);
    public Task<IEnumerable<Game?>> GetUsersLibrary(int userId);
    public Task<bool> HasUserGame(User? user, Game? game);
}
