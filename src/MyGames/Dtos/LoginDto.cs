using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Dtos
{
    public class LoginDto
    {
        public string? Login{set; get;}
        public string? Password{set; get;}
        public string? ReturnUrl { get; set; }
    }
}