using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Services.Base;

namespace MyGames.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository repository;
        public LogService(ILogRepository repository) {
            this.repository = repository;
        }
        public async Task CreateLogAsync(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            await repository.CreateAsync(log);
        }
    }
}