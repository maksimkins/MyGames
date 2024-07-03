using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Dtos;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Services.Base;

namespace MyGames.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        public UserService(IUserRepository repository) {
            this.repository = repository;
        }
        public async Task CreateAsync(RegistrationDto user)
        {
            try
            {
                if(user == null || user.Email == null || user.Username == null || user.Password == null || user.Login == null)
                {
                    throw new ArgumentNullException("null params");
                }  
    
                await repository.CreateAsync(new User(){
                    Login = user.Login,
                    Password = user.Password,
                    Username = user.Username,
                    Birthdate = user.Birthdate,
                    Email = user.Email,
                });
            }
            catch(ArgumentNullException ex)
            {
                throw ex;
            }
            catch(Exception)
            {
                throw new ArgumentException("user with this email or login already exists");
            }
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await repository.GetByIdAsync(id);
        }

        public async Task<User?> Login(LoginDto userToLogin)
        {
            if (string.IsNullOrEmpty(userToLogin.Login) || string.IsNullOrEmpty(userToLogin.Password))
            {
                throw new ArgumentNullException(nameof(userToLogin));
            }

            return await repository.Login(userToLogin);
        }
    }
}