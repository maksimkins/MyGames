using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;

namespace MyGames.Infrastructure.Game.Repositories.Dapper;

using MyGames.Core.Options.Base;
using MyGames.Core.Options.MsSqlConnection;
using MyGames.Core.Game.Repositories.Base;
using MyGames.Core.Game.Models;
using MyGames.Core.User.Models;

public class GameDapperRepository : IGameRepository
{
    private readonly IConnectionStringOption connectionStringOptions;

    public GameDapperRepository(IOptionsSnapshot<MsSqlConnectionOptions> option)
    {
        connectionStringOptions = option.Value;
    }

    public Task DeleteAsync(Game? entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        var connection = new SqlConnection(connectionStringOptions.ConnectionString);
        var games = await connection.QueryAsync<Game>(@"select * from Games");

        return games;
    }

    public Task AddToLibrary(int userId, int gameId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Game> GetAllFromUserLibrary(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Game?>> GetAllFromUserLibraryAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<Game?> GetByIdAsync(int id)
    { 
        var connection = new SqlConnection(connectionStringOptions.ConnectionString);
        var game = await connection.QueryFirstOrDefaultAsync<Game>(@"select * 
                                                from Games 
                                                where id = @id", new {
                                                    id = id
                                                });
        
        return game;
    }

    public Task<IEnumerable<Game?>> GetTopTenNewest()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Game?>> GetTopTenMostHighRated()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Game?>> GetGamesPagination(int page = 1, int pageSize = 10)
    {
        throw new NotImplementedException();
    }
}
