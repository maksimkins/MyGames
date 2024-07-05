using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base.Methods;

namespace MyGames.Repositories.Base
{
    public interface IUserGameRepository : ICreateAsync<UserGame>, IDeleteAsync<UserGame>
    {
        public Task<IEnumerable<UserGame?>> GetAllUserGames(int userId);
        public Task<bool> HasUserGame(int gameId, int userId);
    }
}