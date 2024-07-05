using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;

namespace MyGames.Services.Base
{
    public interface IUserGameService
    {
        public Task BuyAsync(User? user, Game? game);
        public Task<IEnumerable<Game?>> GetAllUsersGames(int userId);
        public Task<bool> HasUserGame(User? user, Game? game);
    }
}