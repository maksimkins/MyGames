using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
    }
}