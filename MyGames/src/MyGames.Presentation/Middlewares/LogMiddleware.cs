using System.Text;
using Microsoft.AspNetCore.Http.Extensions;

namespace MyGames.Presentation.Middlewares;

using MyGames.Core.Log.Services.Base;
using MyGames.Core.Log.Models;

public class LogMiddleware : IMiddleware
{
    private readonly ILogService service;
    public LogMiddleware(ILogService service, IConfiguration configuration) 
    {
        this.service = service;
        isLogging = configuration.GetSection("GeneralOptions:isLogging")?.Get<bool>() ?? false;
    }

    public bool isLogging { get; set; }

    private async Task<string> ReadRequestBody(HttpContext context) 
    {
        
        context.Request.EnableBuffering();
        string requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
        context.Request.Body.Position = 0;

        return requestBody;
    }

    private async Task<string> ReadResponseBody(HttpContext context)
    {
        Stream originalBody = context.Response.Body;
        using var memStream = new MemoryStream();
        context.Response.Body = memStream;

        memStream.Position = 0;
        var streamReader = new StreamReader(memStream);
        string responseBody = await streamReader.ReadToEndAsync();

        memStream.Position = 0;
        await memStream.CopyToAsync(originalBody);
        context.Response.Body = originalBody;

        return responseBody;
    }

    private async void Initialize(Log log, HttpContext context)
    {
        if(log is null)
        {
            throw new ArgumentNullException(nameof(log));
        }

        log.CreationDate = DateTime.Now;
        log.Url = context.Request.GetDisplayUrl();

        log.RequestBody = await this.ReadRequestBody(context);
    }

    private async void Finish(Log log, HttpContext context)
    {
        if(log is null)
        {
            throw new ArgumentNullException(nameof(log));
        }

        log.ResponsetBody = await this.ReadResponseBody(context);

        log.StatusCode = context.Response.StatusCode;
        log.HttpMethod = context.Request.Method;
        log.EndDate = DateTime.Now;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if(!isLogging)
        {
            await next.Invoke(context);
            return;
        }

        var log = new Log();
        try
        {
            this.Initialize(log, context);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

        await next.Invoke(context);

        try
        {
            this.Finish(log, context);
            await service.CreateLogAsync(log);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }
}
