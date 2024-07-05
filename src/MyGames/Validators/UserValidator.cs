using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MyGames.Models;

namespace MyGames.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {
            RuleFor<string?>((u) => u.Login)
                .NotEmpty().WithMessage("Your login cannot be empty.")
                .NotNull().WithMessage("Your login cannot be empty.");

            RuleFor<DateTime?>((u) => u.Birthdate)
                .NotEmpty().WithMessage("Your birthdate cannot be empty.")
                .NotNull().WithMessage("Your birthdate cannot be empty.");
        
            RuleFor<string?>((u) => u.Email)
                .NotEmpty().WithMessage("Your email cannot be empty.")
                .NotNull().WithMessage("Your email cannot be empty.");
        }
    }
}