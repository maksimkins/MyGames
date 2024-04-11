
using System.Text.Json.Serialization;

namespace MyGames.Models;

public class Game {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name {set; get;}
    [JsonPropertyName("price")]
    public decimal? Price{set; get;}
}