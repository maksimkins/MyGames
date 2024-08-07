using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base.Methods;

namespace MyGames.Repositories.Base
{
    public interface ICommentRepository : ICreateAsync<Comment>, IDeleteAsync<Comment>, IChangeAsync<Comment>, IGetByIdAsync<Comment?>
    {
        public Task<IEnumerable<Comment>> GetAllByGameAsync(int gameId);
    }
}