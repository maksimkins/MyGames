using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Models
{
    public class Comment
    {
        public int? Id { get; set; }
        public int? GameId { get; set; }
        public string? Title {set; get;}
        public string? Text {set; get;}
    }
}