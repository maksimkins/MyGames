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
using MyGames.Core.UserGame.Repositories.Base;
using MyGames.Core.UserGame.Services.Base;
using MyGames.Infrastructure.UserGame.Services;
using MyGames.Infrastructure.UserGame.Repositories.Ef_Core;
using MyGames.Core.Role.Services;
using MyGames.Infrastructure.Roles.Services;
using MyGames.Core.Common.Admin.Services;
using MyGames.Infrastructure.Common.Admin.Services;
using Microsoft.OpenApi.Models;


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

builder.Services.AddScoped<IUserGameRepository, UserGameEFCoreRepository>();
builder.Services.AddScoped<IUserGameService, UserGameService>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAdminService, AdminService>();

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
        policyBuilder.RequireAuthenticatedUser();
        policyBuilder.RequireClaim("IsMuted", "False");
    });

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Cookie,
        Name = "AspIdentityCookie",
        Scheme = "cookieAuth",
        Description = "Authorization using a cookie scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "cookieAuth"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<MyGamesDbContext>();

    dbContext.Database.Migrate();
    dbContext.Database.ExecuteSqlRaw(@"
    
use MyGamesDb;

insert into Games(Rate, Name, Description, Price, CreationDate, ForAdultsOnly, PictureUrl)
 values
 (3.5,'Hogwarts Legacy', 'Хогвартс. Наследие – это ролевая игра с открытым миром. Теперь вы можете контролировать свои действия и стать центральным героем собственных приключений в волшебном мире.', 50, '2012-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/990080/header.jpg?t=1717689083'),
 (4.5, 'PAYDAY 2', 'PAYDAY 2 - это кооперативный экшн-шутер для четверых игроков, который снова позволяет игрокам надеть маски оригинальной банды PAYDAY - Даллас, Хокстон, Чейнс и Вулф, которые прибыли в Вашингтон для новой крутой серии преступлений.', 5, '2017-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/218620/header_alt_assets_1.jpg?t=1718698928'),
 (3.2, 'PAYDAY 3', 'PAYDAY 3 is the much anticipated sequel to one of the most popular co-op shooters ever. Since its release, PAYDAY-players have been reveling in the thrill of a perfectly planned and executed heist. That’s what makes PAYDAY a high-octane, co-op FPS experience without equal.', 50, '2005-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1272080/header_alt_assets_4.jpg?t=1720106055'),
 (4.6, 'Metro Exodus', 'Оставьте позади руины московского метро и снова отправляйтесь в путешествие по постапокалиптическим землям России. Вас ждут большие нелинейные уровни, открытый мир и захватывающая сюжетная линия.', 17, '2007-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/412020/header.jpg?t=1706778291'),
 (3.2, 'Baldur Gate 3', 'Соберите отряд и вернитесь в Забытые Королевства. Вас ждет история о дружбе и предательстве, выживании и самопожертвовании, о сладком зове абсолютной власти.', 50, '2024-07-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/header.jpg?t=1713271288'),
 (5, 'LEGO Star Wars', 'Kick Some Brick in I through VI! Play through all six Star Wars movies in one videogame! Adding new characters, new levels, new features and for the first time ever, the chance to build and battle your way through a fun Star Wars galaxy on your PC!', 50, '2003-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/32440/header.jpg?t=1604517910'),
 (4, 'Devil May Cry 5', 'Лучший охотник на демонов возвращается в новом стильном боевике.', 18, '2024-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/601150/header.jpg?t=1701395090'),
 (5,'ELDEN RING Shadow of the Erdtree', 'iga-bombma', 50, '2014-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/2778580/header_alt_assets_1.jpg?t=1720108810'),
 (4,'Sons Of The Forest', 'Sent to find a missing billionaire on a remote island, you find yourself in a cannibal-infested hellscape. Craft, build, and struggle to survive, alone or with friends, in this terrifying new open-world survival horror simulator.', 30, '2010-05-19', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1326470/header.jpg?t=1708624856'),
 (4,'Terraria', 'sandbox from independent developers', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/105600/header.jpg?t=1666290860'),
 (4,'The Elder Scrolls V: Skyrim Special Edition', 'В игре The Elder Scrolls V: Skyrim Special Edition, получившей более 200 наград «Игра года», вас ждет удивительный мир, воссозданный с потрясающей детализацией. В издание Special Edition вошли базовая игра и дополнения, добавляющие в нее ряд новых возможностей.', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/489830/header.jpg?t=1717972262'),
 (4,'Battlefield 2042', 'Сезон 7: «Точка перелома». Всё или ничего. Battlefield™ 2042 — это шутер от первого лица, в котором серия возвращается к легендарным масштабным сражениям.', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1517290/header.jpg?t=1718115687'),
 (2,'Grand Theft Auto V', 'Grand Theft Auto V для PC позволяет игрокам исследовать знаменитый мир Лос-Сантоса и округа Блэйн в разрешении до 4k и выше с частотой 60 кадров в секунду.', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/271590/header.jpg?t=1716224849'),
 (1,'Sid Meier’s Civilization VI', 'Сыграйте за одного из 20 лидеров – например за Петра Великого, российского императора.', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/289070/header.jpg?t=1719520634')

    ");
}

using (var scope = app.Services.CreateScope())
{
    var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
    await roleService.SetupRolesAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

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
