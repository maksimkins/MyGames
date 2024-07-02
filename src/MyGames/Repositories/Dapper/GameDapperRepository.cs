using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using MyGames.Models;
using MyGames.Options;
using MyGames.Options.Base;
using MyGames.Repositories.Base;

namespace MyGames.Repositories.Dapper
{
    public class GameDapperRepository : IGameRepository
    {
        private readonly IConnectionStringOption connectionStringOptions;

        public GameDapperRepository(IOptionsSnapshot<MsSqlconnectionOptions> option)
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

        public IEnumerable<Game> GetAllFromUserLibrary(User user)
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
    }
}