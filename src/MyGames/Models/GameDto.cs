
using System.Text.Json.Serialization;

namespace MyGames.Models;

public class GameDto {

    [JsonPropertyName("name")]
    public string? Name {set; get;}
    // [JsonPropertyName("price")]
    // public decimal? Price{set; get;}
}