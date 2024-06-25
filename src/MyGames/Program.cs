using System.Reflection;
using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyGames.Middlewares;
using MyGames.Models;
using MyGames.Options;
using MyGames.Repositories.Base;
using MyGames.Repositories.Dapper;
using MyGames.Repositories.EF_Core;
using MyGames.Repositories.EF_Core.DbContext;
using MyGames.Services;
using MyGames.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyGamesDbContext>();

builder.Services.AddScoped<IGameRepository, GameEFCoreRepository>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<IUserRepository, UserEFCoreRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICommentRepository, CommentEFCoreRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IUserRepository, UserEFCoreRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ILogRepository, LogDapperRepository>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<LogMiddleware>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var connectionStringSection = builder.Configuration.GetSection("connections:MsSql");
builder.Services.Configure<MsSqlconnectionOptions>(connectionStringSection);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Login";
        options.AccessDeniedPath = "/api/Identity/Logout";
    });

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("MyPolicy", policyBuilder =>
    {
        policyBuilder.RequireClaim(ClaimTypes.Role, "User", "Developer");
    });

});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LogMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
