using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base.Methods;

namespace MyGames.Repositories.Base
{
    public interface IUserGameRepository : ICreateAsync<UserGame>
    {
        public Task<IEnumerable<Game?>> GetAllUsersGames(int userId);
        public Task<bool> HasUserGame(int gameId, int userId);
    }
}