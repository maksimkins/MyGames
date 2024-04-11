
using System.Text.Json.Serialization;

namespace MyGames.Models;

public class User {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name {set; get;}
    [JsonPropertyName("surname")]
    public string? Surname{set; get;}
    [JsonPropertyName("birthdate")]
    public DateTime? Birthdate {set; get;}
}