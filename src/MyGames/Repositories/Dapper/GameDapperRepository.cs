using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyGames.Models;
using MyGames.Repositories.Base;

namespace MyGames.Repositories.Dapper
{
    public class GameDapperRepository : IGameRepository
    {
        private const string connectionString = "Database=MyGame;Server=localhost;TrustServerCertificate=True;";
        public Task<IEnumerable<Game>> GetAllAsync()
        {
            var connection = new SqlConnection(connectionString);
            var games = connection.QueryAsync<Game>(@"select * from Games");

            connection.Dispose();

            return games;
        }

        public Task<Game?> GetByIdAsync(int id)
        {
            var connection = new SqlConnection(connectionString);
            var game = connection.QueryFirstOrDefaultAsync<Game>(@"select * 
                                                    from Games 
                                                    where id = @id", id);

            connection.Dispose();
            
            return game;
        }
    }
}