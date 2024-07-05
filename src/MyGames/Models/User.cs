using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MyGames.Models
{
    public class User : IdentityUser<int>
    {
        [Required, MinLength(5), MaxLength(15)] 
        public string? Login{set; get;}
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Balance{set; get;}
        [Required]
        public DateTime? Birthdate {set; get;}
        public string? AvatarUrl {set; get;}
        public IEnumerable<UserGame>? Games {set; get;}
    }
}