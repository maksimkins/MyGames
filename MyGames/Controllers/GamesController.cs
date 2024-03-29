namespace MyGames.Controllers;

using System.Net;
using System.Text;
using System.Text.Json;
using MyGames.Models;
using MyGames.Repositories.EF_Core;

public class GamesController {
    private readonly GamesEFCoreRep rep;

    public GamesController() {
        rep = new GamesEFCoreRep();
    }

    private string HtmlUsers(IEnumerable<Game> games) {
        var html = File.ReadAllText("./Views/index.html");

        if(games is null) {
            return html;
        }

        var gamesHtml = new StringBuilder();

        foreach(var game in games) {
            gamesHtml.Append($"\nname: {game.Name}\nprice: {game.Price}\n");
        }

        gamesHtml.Append("<br/>");

        var newstr =  html.Replace("{{body}}", gamesHtml.ToString());

        System.Console.WriteLine(newstr);

        return newstr;
    }

    public void Index(HttpListenerContext? client) {

        var methodType = client?.Request.HttpMethod ?? throw new ArgumentException("client is null (HttpListenerContext)"); 

        switch(methodType) {

            case("GET"):{
                var games = rep.GetAll();
                client.Response.ContentType = "text/html";
                var html = this.HtmlUsers(games);
                client.Response.OutputStream.Write(Encoding.ASCII.GetBytes(html), 0, html.Length);
                client.Response.StatusCode = (int)HttpStatusCode.OK;

                break;
            }


            default: {
                var error = "not found";
                client.Response.OutputStream.Write(Encoding.ASCII.GetBytes(error), 0, error.Length);
                client.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            }
            
        }
    }
}