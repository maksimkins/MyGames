using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;

namespace MyGames.Services.Base
{
    public interface ILogService
    {
        public Task CreateLogAsync(Log log);
    }
}