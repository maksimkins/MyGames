
using System.Text.Json.Serialization;

namespace MyGames.Models;

public class Game {
    public int? Id { get; set; }
    public string? Name {set; get;}
    public string? Description {set; get;}
    public decimal? Price{set; get;}
}