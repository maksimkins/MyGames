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
    public class CommentDapperRepository : ICommentRepository
    {
        private readonly IConnectionStringOption connectionStringOptions;

        public CommentDapperRepository(IOptionsSnapshot<MsSqlconnectionOptions> option)
        {
            connectionStringOptions = option.Value;
        }
        public async Task ChangeAsync(int id, Comment comment)
        {
            comment.Id = id;
            var connection = new SqlConnection(connectionStringOptions.ConnectionString);
            await connection.ExecuteAsync(@"update Comments 
                                        set 
                                        Title = @Title,
                                        Text = @Text,
                                        ChangeDate = @ChangeDate,
                                        Rate = @Rate
                                        where Id = @Id", comment);
        }

        public async Task CreateAsync(Comment comment)
        {
            var connection = new SqlConnection(connectionStringOptions.ConnectionString);
            await connection.ExecuteAsync(@"insert into Comments
                                        (Text, Title, GameId, CreationDate, ChangeDate, Rate)
                                        values (@Text, @Title, @GameId, @CreationDate, @ChangeDate, @Rate)", comment);
        }

        public async Task DeleteAsync(Comment comment)
        {
            var connection = new SqlConnection(connectionStringOptions.ConnectionString);
            await connection.ExecuteAsync(@"delete from Comments
                                        where Id = @Id", comment);
        }

        public async Task<IEnumerable<Comment>> GetAllByGameAsync(int gameId)
        {
            var connection = new SqlConnection(connectionStringOptions.ConnectionString);
            var comments = await connection.QueryAsync<Comment>(@"select *
                                                            from Comments
                                                            where GameId = @gameId", new {
                                                                gameId = gameId
                                                                });

            return comments;
        }
    }
}