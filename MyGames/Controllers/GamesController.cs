namespace MyGames.Controllers;

using System.Net;
using System.Text;
using System.Text.Json;
using MyGames.Repositories.EF_Core;

public class GamesController {
    private readonly GamesEFCoreRep rep;

    public GamesController() {
        rep = new GamesEFCoreRep();
    }

    public void RequestVerifier(HttpListenerContext? client) {

        var methodType = client?.Request.HttpMethod ?? throw new ArgumentException("client is null (HttpListenerContext)"); 

        switch(methodType) {

            case("GET"):{
                var games = rep.GetAll();
                client.Response.ContentType = "Application/json";
                var gamesJson = JsonSerializer.Serialize(games);
                client.Response.OutputStream.Write(Encoding.ASCII.GetBytes(gamesJson), 0, gamesJson.Length);
                client.Response.StatusCode = (int)HttpStatusCode.OK;
                client.Response.Close();
                break;
            }


            default: {
                var error = "not found";
                client.Response.OutputStream.Write(Encoding.ASCII.GetBytes(error), 0, error.Length);
                client.Response.StatusCode = (int)HttpStatusCode.NotFound;
                client.Response.Close();
                break;
            }
            
        }
    }
}