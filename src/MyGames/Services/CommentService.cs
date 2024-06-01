using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Services.Base;


namespace MyGames.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repository;
        public CommentService(ICommentRepository repository) {
            this.repository = repository;
        }
        public async Task ChangeCommentAsync(int id, Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            
            await repository.ChangeAsync(id, comment);
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            if (comment == null || comment.GameId == null || comment.Title == null || comment.Text == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            comment.CreationDate = DateTime.Now;
            await repository.CreateAsync(comment);
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            if (comment == null || comment.Id == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            await repository.DeleteAsync(comment);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByGameAsync(int gameId)
        {
            if (gameId <= 0)
            {
                throw new ArgumentNullException(nameof(gameId));
            }

            return await repository.GetAllByGameAsync(gameId);
        }
    }
}