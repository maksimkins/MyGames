using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Repositories.EF_Core.DbContext;

namespace MyGames.Repositories.EF_Core;

#pragma warning disable CS1998

public class CommentEFCoreRepository : ICommentRepository
{
    private readonly MyGamesDbContext dbContext;
    public CommentEFCoreRepository(MyGamesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task ChangeAsync(int id, Comment comment)
    {
        comment.Id = id;
        dbContext.Update(comment);

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
        return dbContext.Comments.Where(c => c.GameId == gameId);
    }
}
