namespace MyGames.Core.Game.Repositories.Base;

using MyGames.Core.Base.Methods;
using MyGames.Core.Game.Models;
using MyGames.Core.User.Models;

public interface IGameRepository : IGetAllAsync<Game>, IGetByIdAsync<Game?>, IDeleteAsync<Game?>
{
    public Task<IEnumerable<Game?>> GetAllFromUserLibraryAsync(User user);
    public Task<IEnumerable<Game?>> GetTopTenNewest();
    public Task<IEnumerable<Game?>> GetTopTenMostHighRated();
    public Task<IEnumerable<Game?>> GetGamesPagination(int page = 1, int pageSize = 10);
}
