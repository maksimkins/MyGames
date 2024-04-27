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
        private readonly IGameRepository repository;
        public GameService(IGameRepository repository) {
            this.repository = repository;
        }
        public Task<IEnumerable<Game>> AllGamesAsync()
        {
            return repository.GetAllAsync();
        }

        public Task<Game?> GameByIdAsync(int id)
        {
            return repository.GetByIdAsync(id);
        }
    }
}