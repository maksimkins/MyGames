#pragma warning disable CS1998

using Microsoft.EntityFrameworkCore;

namespace MyGames.Infrastructure.Comment.Repositories.Ef_Core;

using MyGames.Core.Comment.Repositories.Base;
using MyGames.Core.Data.DbContext;
using MyGames.Core.Comment.Models;

public class CommentEFCoreRepository : ICommentRepository
{
    private readonly MyGamesDbContext dbContext;
    public CommentEFCoreRepository(MyGamesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task ChangeAsync(int id, Comment comment)
    {
        var commentToChange = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        
        if(commentToChange is null)   
            return;
       
        commentToChange.Text = comment.Text;

        dbContext.Update(commentToChange);

        await dbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(Comment comment)
    {
        await dbContext.Comments.AddAsync(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment comment)
    {
        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Comment>> GetAllByGameAsync(int gameId)
    {
        return dbContext.Comments.Where(c => c.GameId == gameId).Include(c => c.User);
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }
}
