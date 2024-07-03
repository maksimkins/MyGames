using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyGames.Models
{
    public class User
    {
        [Key]
        public int? Id { get; set; }
        [Required, MinLength(5), MaxLength(15)] 
        public string? Login{set; get;}
        [Required, MinLength(5), MaxLength(18)]
        public string? Password{set; get;}
        [Required] 
        public string? Email{set; get;}
        [Required, MinLength(5), MaxLength(18)]
        public string? Username {set; get;}
        public decimal? Balance{set; get;}
        [Required]
        public DateTime? Birthdate {set; get;}
        public string? AvatarUrl{set; get;}
    }
}