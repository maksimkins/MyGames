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
                .NotEmpty()
                .NotNull();

            RuleFor<DateTime?>((u) => u.Birthdate)
                .NotEmpty()
                .NotNull();
        
            RuleFor<string?>((u) => u.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(320);

            RuleFor<string?>((u) => u.Username)
                .NotEmpty()
                .NotNull()
                .MaximumLength(60);

            RuleFor<string?>((u) => u.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(60);
        }
    }
}