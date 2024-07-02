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
        public async Task<IEnumerable<Game>> AllGamesAsync()
        {   
            return await repository.GetAllAsync();
        }

        public async Task DeleteGameAsync(Game? game)
        {
            if (game == null || game.Id == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            await repository.DeleteAsync(game);
        }

        public async Task<Game?> GameByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Game>> GetAllFromUserLibraryAsync(User user)
        {
            if(user.Id == null)
            {
                throw new ArgumentNullException("user Id is null, couldn't find games");
            }
            return await repository.GetAllFromUserLibraryAsync(user);
        }
    }
}