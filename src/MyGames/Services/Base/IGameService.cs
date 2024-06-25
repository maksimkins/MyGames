using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;

namespace MyGames.Services.Base
{
    public interface IGameService
    {
        public Task<IEnumerable<Game>> AllGamesAsync();
        public Task<Game?> GameByIdAsync(int id);
        public Task DeleteGameAsync(Game? game);
    }
}