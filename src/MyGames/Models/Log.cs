using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Models
{
    public class Log
    {
        public string? Url { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponsetBody { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusCode { get; set; }
        public string? HttpMethod { get; set; }
    }
}

