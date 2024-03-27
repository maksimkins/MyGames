namespace MyGames;

using System.Net;
using System.Text.Json;
using MyGames.Models;
using MyGames.Repositories.EF_Core;

public class Program
{
    public async static void Main(string[] args)
    {
        // GamesEFCoreRep rep = new GamesEFCoreRep();
        // var games = rep.GetAll();

        // foreach(var game in games) {
        //     System.Console.WriteLine(game.Name);
        // }

        var httpListener = new HttpListener();
        var prefix = "http://*:8080/";
        httpListener.Prefixes.Add(prefix);
        httpListener.Start();

        System.Console.WriteLine($"Server started... {prefix.Replace("*", "localhost")}");


//         while (true)
// {
//     var client = await httpListener.GetContextAsync();

//     string? endpoint = client.Request.RawUrl;

//     switch (endpoint)
//     {
//         case "/":
//             {
//                 await WriteViewAsync(client.Response, "index");
//                 break;
//             }
//         case "/Users":
//             {
//                 var usersJson = await File.ReadAllTextAsync("users.json");
//                 var users = JsonSerializer.Deserialize<IEnumerable<User>>(usersJson);
                
//                 if(users is not null && users.Any()) {
//                     var html = users.AsHtml();
//                     await LayoutAsync(client.Response, html);
//                 }
//                 else {
//                     await NotFoundAsync(client.Response, nameof(users));
//                 }

//                 break;
//             }
//         default:
//             {
//                 await NotFoundAsync(client.Response, endpoint!);

//                 break;
//             }
//     }

//     client.Response.Close();
// }
    }
}
