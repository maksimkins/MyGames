using MyGames.Models;
using MyGames.Options;
using MyGames.Repositories.Base;
using MyGames.Repositories.Dapper;
using MyGames.Services;
using MyGames.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGameRepository, GameDapperRepository>();
builder.Services.AddScoped<ICommentRepository, CommentDapperRepository>();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ICommentService, CommentService>();

var connectionStringSection = builder.Configuration.GetSection("connections:MsSql");
builder.Services.Configure<MsSqlconnectionOptions>(connectionStringSection);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
