using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Models
{
    public class UserGame
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId"), Required]
        public int? UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey("GameId"), Required]
        public int? GameId { get; set; }
        public Game? Game { get; set; }
    }
}