using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base.Methods;

namespace MyGames.Repositories.Base
{
    public interface IGameRepository : IGetAllAsync<Game>, IGetByIdAsync<Game?>, IDeleteAsync<Game?>
    {
        public Task<IEnumerable<Game?>> GetAllFromUserLibraryAsync(User user);
        public Task<IEnumerable<Game?>> GetTopTenNewest();
        public Task<IEnumerable<Game?>> GetTopTenMostHighRated();
        public Task<IEnumerable<Game?>> GetGamesPagination(int page = 1, int pageSize = 10);
    }
}