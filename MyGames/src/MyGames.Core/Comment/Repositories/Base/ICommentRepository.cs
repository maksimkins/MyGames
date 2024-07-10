namespace MyGames.Core.Comment.Repositories.Base;

using MyGames.Core.Base.Methods;
using MyGames.Core.Comment.Models;

public interface ICommentRepository : ICreateAsync<Comment>, IDeleteAsync<Comment>, IChangeAsync<Comment>, IGetByIdAsync<Comment?>
{
    public Task<IEnumerable<Comment>> GetAllByGameAsync(int gameId);
}
