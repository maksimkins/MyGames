using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Repositories.Base.Methods
{
    public interface IChange<TEntity>
    {
        public void Сhange(TEntity entity);
    }
}