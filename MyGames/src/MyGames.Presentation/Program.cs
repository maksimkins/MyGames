using MyGames.Infrastructure.Data.DbContext;

using MyGames.Core.Options.MsSqlConnection;

using MyGames.Core.User.Models;
using MyGames.Core.Role.Models;

using MyGames.Core.Comment.Repositories.Base;
using MyGames.Core.Comment.Services.Base;

using MyGames.Core.Game.Repositories.Base;
using MyGames.Core.Game.Services.Base;

using MyGames.Core.Log.Repositories.Base;
using MyGames.Core.Log.Services.Base;

using MyGames.Infrastructure.Game.Repositories.Ef_Core;
using MyGames.Infrastructure.Game.Services;

using MyGames.Infrastructure.Comment.Repositories.Ef_Core;
using MyGames.Infrastructure.Comment.Services;

using MyGames.Infrastructure.Log.Repositories.Dapper;
using MyGames.Infrastructure.Log.Services;

using MyGames.Presentation.Middlewares;

using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyGamesDbContext>( (dbContext) =>{
    dbContext.UseSqlServer(
        // x => x.MigrationsAssembly("WebApplication1.Migrations")
    );
});

builder.Services.AddScoped<IGameRepository, GameEFCoreRepository>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<ICommentRepository, CommentEFCoreRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<ILogRepository, LogDapperRepository>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddIdentity<User, Role>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<MyGamesDbContext>();

builder.Services.AddScoped<LogMiddleware>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var connectionStringSection = builder.Configuration.GetSection("connections:MsSql");
builder.Services.Configure<MsSqlConnectionOptions>(connectionStringSection);

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
        policyBuilder.RequireClaim(ClaimTypes.Role, "User", "Developer", "Admin");
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
