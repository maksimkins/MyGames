namespace MyGames;

using System.Net;
using System.Text.Json;
using MyGames.Controllers;
using MyGames.Models;
using MyGames.Repositories.EF_Core;

public class Program
{
    public async static Task Main(string[] args)
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

        var controller = new GamesController();

        while (true){

            var client = await httpListener.GetContextAsync();

            string? endpoint = client.Request.RawUrl;

            if(endpoint == "/Users") {
                controller.RequestVerifier(client);
            }

            client.Response.Close();
        }
    }
}
