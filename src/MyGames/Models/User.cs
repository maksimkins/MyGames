using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyGames.Models
{
    public class User
    {
        [Key]
        public int? Id { get; set; }
        [Required] 
        public string? Login{set; get;}
        [Required] [MaxLength(60)]
        public string? Password{set; get;}
        [Required] [MaxLength(320)]
        public string? Email{set; get;}
        [Required] [MaxLength(60)]
        public string? Username {set; get;}
        public decimal? Balance{set; get;}
        [Required]
        public DateTime? Birthdate {set; get;}
        public string? AvatarUrl{set; get;}
        public IEnumerable<Game> Games { get; set; } = Enumerable.Empty<Game>();
    }
}