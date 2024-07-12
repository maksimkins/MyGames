using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;

namespace MyGames.Services.Base
{
    public interface ICommentService
    {
        public Task ChangeCommentAsync(int userId, int id, Comment comment);
        public Task CreateCommentAsync(Comment comment);
        public Task DeleteCommentAsync(int userId, Comment comment);
        public Task<IEnumerable<Comment>> GetCommentsByGameAsync(int gameId);
    }
}