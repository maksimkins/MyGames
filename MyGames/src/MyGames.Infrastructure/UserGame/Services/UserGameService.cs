namespace MyGames.Infrastructure.UserGame.Services;

using MyGames.Core.UserGame.Services.Base;
using MyGames.Core.UserGame.Models;
using MyGames.Core.User.Models;
using MyGames.Core.Game.Models;
using MyGames.Core.UserGame.Repositories.Base;

public class UserGameService : IUserGameService
{
    private readonly IUserGameRepository repository;
    public UserGameService(IUserGameRepository repository) {
        this.repository = repository;
    }
    public async Task BuyAsync(User? user, Game? game)
    {
        if(user is null || game is null || user.Id <= 0 || game.Id is null)
        {
            throw new ArgumentNullException("params are null, cannot buy game");
        }

        var userGame = new UserGame(){
            UserId = user.Id,
            GameId = game.Id,
        };

        await repository.CreateAsync(userGame);
    }

    public async Task<IEnumerable<Game?>> GetUsersLibrary(int userId)
    {
        if(userId <= 0)
        {
            throw new ArgumentException("userId is zero or below");
        }
        
        return await repository.GetAllUsersGames(userId);
    }

    public async Task<bool> HasUserGame(User? user, Game? game)
    {
        if(user is null || game is null || user.Id <= 0 || game.Id is null)
        {
            throw new ArgumentNullException("params are null, cannot check if user has particular game");
        }

        return await repository.HasUserGame(user.Id, game.Id.Value); 
    }
}
