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
        public async Task<IEnumerable<Game?>> AllGamesAsync()
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

        public async Task<IEnumerable<Game?>> GetAllFromUserLibraryAsync(User user)
        {
            if(user.Id <= 0)
            {
                throw new ArgumentNullException("not proper user id");
            }
            return await repository.GetAllFromUserLibraryAsync(user);
        }

        public async Task<IEnumerable<Game?>> GetGamesPagination(int page = 1, int pageSize = 10)
        {
            if(page < 1 || pageSize < 1 || pageSize > 100)
            {
                throw new ArgumentException("wrong pagination params");
            }

            return await repository.GetGamesPagination(page, pageSize);
        }

        public async Task<IEnumerable<Game?>> GetTopTenMostHighRated()
        {
            return await repository.GetTopTenMostHighRated();
        }

        public async Task<IEnumerable<Game?>> GetTopTenNewest()
        {
            return await repository.GetTopTenNewest();
        }
    }
}