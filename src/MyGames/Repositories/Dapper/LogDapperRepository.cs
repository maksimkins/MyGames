using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using MyGames.Models;
using MyGames.Options;
using MyGames.Options.Base;
using MyGames.Repositories.Base;

namespace MyGames.Repositories.Dapper
{
    public class LogDapperRepository : ILogRepository
    {
        private readonly IConnectionStringOption connectionStringOptions;

        public LogDapperRepository(IOptionsSnapshot<MsSqlconnectionOptions> option)
        {
            connectionStringOptions = option.Value;
        }

        public async Task CreateAsync(Log log)
        {
            var connection = new SqlConnection(connectionStringOptions.ConnectionString);
            await connection.ExecuteAsync(@"insert into Logs
                                        (Url, RequestBody, ResponsetBody, CreationDate, EndDate, StatusCode, HttpMethod)
                                        values (@Url, @RequestBody, @ResponsetBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)", log);
        }
    }
}