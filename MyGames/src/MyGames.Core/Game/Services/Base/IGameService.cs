namespace MyGames.Core.Game.Services.Base;

using MyGames.Core.Game.Models;
using MyGames.Core.User.Models;

public interface IGameService
{
    public Task<IEnumerable<Game?>> AllGamesAsync();
    public Task<Game?> GameByIdAsync(int id);
    public Task DeleteGameAsync(Game? game);
    public Task<IEnumerable<Game?>> GetAllFromUserLibraryAsync(User user);
    public Task<IEnumerable<Game?>> GetTopTenNewest();
    public Task<IEnumerable<Game?>> GetTopTenMostHighRated();
    public Task<IEnumerable<Game?>> GetGamesPagination(int page = 1, int pageSize = 10);
}