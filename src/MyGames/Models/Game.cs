
using System.Text.Json.Serialization;

namespace MyGames.Models;

public class Game {
    public int? Id { get; set; }
    public int? SerieId { get; set; }
    public int? DeveloperId { get; set; }
    public string? Name {set; get;}
    public string? Description {set; get;}
    public decimal? Price{set; get;}
    public DateTime? CreationDate{set; get;}
    public float? Rate{set; get;}
    public bool? ForAdultsOnly{set; get;}
    public string? PictureUrl{set; get;}
}