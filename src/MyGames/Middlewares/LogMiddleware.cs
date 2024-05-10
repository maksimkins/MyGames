using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Services.Base;

namespace MyGames.Middlewares
{
    public class LogMiddleware : IMiddleware
    {
        private readonly ILogService service;
        public LogMiddleware(ILogService service) 
        {
            this.service = service;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next.Invoke(context);
        }
    }
}