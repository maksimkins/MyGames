using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;

namespace MyGames.Infrastructure.Comment.Repositories.Dapper;

using MyGames.Core.Comment.Repositories.Base;
using MyGames.Core.Options.Base;
using MyGames.Core.Options.MsSqlConnection;
using MyGames.Core.Comment.Models;

public class CommentDapperRepository : ICommentRepository
{
    private readonly IConnectionStringOption connectionStringOptions;

    public CommentDapperRepository(IOptionsSnapshot<MsSqlConnectionOptions> option)
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

    public Task<Comment?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
