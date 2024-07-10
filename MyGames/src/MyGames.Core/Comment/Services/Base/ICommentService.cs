namespace MyGames.Core.Comment.Services.Base;

using MyGames.Core.Comment.Models;

public interface ICommentService
{
    public Task ChangeCommentAsync(int userId, int id, Comment comment);
    public Task CreateCommentAsync(Comment comment);
    public Task DeleteCommentAsync(int userId, Comment comment);
    public Task<IEnumerable<Comment>> GetCommentsByGameAsync(int gameId);
}
