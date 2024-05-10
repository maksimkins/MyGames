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
    public class CommentDapperRepository : ICommentRepository
    {
        private const string connectionString = @"Server=localhost;Database=MyGames;Trusted_Connection=True;TrustServerCertificate=True";
        public async Task ChangeAsync(int id, Comment comment)
        {
            comment.Id = id;
            var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"update Comments 
                                        set 
                                        Title = @Title,
                                        Text = @Text
                                        where Id = @id", comment);
        }

        public async Task CreateAsync(Comment comment)
        {
            var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"insert into Comments
                                        (Text, Title, GameId)
                                        values (@Text, @Title, @GameId)", comment);
        }

        public async Task DeleteAsync(Comment comment)
        {
            var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"delete from Comments
                                        where Id = @Id", comment);
        }

        public async Task<IEnumerable<Comment>> GetAllByGameAsync(int gameId)
        {
            var connection = new SqlConnection(connectionString);
            var comments = await connection.QueryAsync<Comment>(@"select *
                                                            from Comments
                                                            where GameId = @gameId", new {
                                                                gameId = gameId
                                                                });

            return comments;
        }
    }
}