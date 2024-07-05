
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyGames.Models;

public class Game {
    [Key]
    public int? Id { get; set; }
    public int? SerieId { get; set; }
    public int? DeveloperId { get; set; }
    [Required]
    public string? Name {set; get;}
    [Required]
    public string? Description {set; get;}
    [Required, Column(TypeName = "decimal(18,2)")]
    public decimal? Price{set; get;}
    [Required]
    public DateTime? CreationDate{set; get;}
    [Range(0, 5), DefaultValue(0), Required]
    public float Rate{set; get;} = 0;
    [DefaultValue(false), Required]
    public bool? ForAdultsOnly{set; get;} = false;
    public string? PictureUrl{set; get;}
    public IEnumerable<UserGame>? Users {set; get;}
}