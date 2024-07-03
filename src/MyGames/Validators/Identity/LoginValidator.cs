using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MyGames.Dtos;

namespace MyGames.Validators.Identity
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor<string?>((u) => u.Login)     
                .NotEmpty().WithMessage("Your login cannot be empty.");
                
            RuleFor<string?>((u) => u.Password)
                .NotEmpty().WithMessage("Your password cannot be empty."); 
        }
    }
}