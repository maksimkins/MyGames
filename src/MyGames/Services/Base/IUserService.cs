using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Dtos;
using MyGames.Models;

namespace MyGames.Services.Base
{
    public interface IUserService
    {
        public Task CreateAsync(RegistrationDto user);

        public Task<User?> GetByIdAsync(int id);

        public Task<User?> Login(LoginDto userToLog);
    }
}