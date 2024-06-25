using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Models
{
    public class Log
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? Url { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponsetBody { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int StatusCode { get; set; }
        [Required]
        public string? HttpMethod { get; set; }
    }
}

