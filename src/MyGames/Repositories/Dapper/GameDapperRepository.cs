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
        private const string connectionString = @"Server=localhost;Database=MyGames;Trusted_Connection=True;TrustServerCertificate=True";
        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            var connection = new SqlConnection(connectionString);
            var games = await connection.QueryAsync<Game>(@"select * from Games");

            return games;
        }

        public async Task<Game?> GetByIdAsync(int id)
        { 
            var connection = new SqlConnection(connectionString);
            var game = await connection.QueryFirstOrDefaultAsync<Game>(@"select * 
                                                    from Games 
                                                    where id = @id", new {
                                                        id = id
                                                    });
            
            return game;
        }
    }
}