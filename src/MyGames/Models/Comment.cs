using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Models;

    public class Comment {
        [Key]
        public int? Id { get; set; }
        [ForeignKey("GameId"), Required]
        public int? GameId { get; set; }
        public Game? Game { get; set; }
        public string? Title {set; get;}
        public string? Text {set; get;}
        [Required]
        public DateTime? CreationDate{set; get;}
        public DateTime? ChangeDate{set; get;}
        [Range(0, 5), Required]
        public float Rate{set; get;} = 0;
        [ForeignKey("UserId"), Required]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
    