using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Services.Base;

#pragma warning disable 1998
#pragma warning disable 4014

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

            repository.ChangeAsync(id, comment);
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            repository.CreateAsync(comment);
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            repository.DeleteAsync(comment);
        }

        public Task<IEnumerable<Comment>> GetCommentsByGameAsync(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            return repository.GetAllByGameAsync(game);
        }
    }
}