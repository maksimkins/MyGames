using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Services.Base;

namespace MyGames.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        public GameService(IGameRepository gameRepository) {
            this.gameRepository = gameRepository;
        }
        public Task<IEnumerable<Game>> AllGamesAsync()
        {
            return gameRepository.GetAllAsync();
        }

        public Task<Game?> GameByIdAsync(int id)
        {
            return gameRepository.GetByIdAsync(id);
        }
    }
}