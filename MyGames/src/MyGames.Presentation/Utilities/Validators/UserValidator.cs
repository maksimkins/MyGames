using FluentValidation;

namespace MyGames.Presentation.Utilities.Validators;

using MyGames.Core.User.Models;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator() {
        RuleFor<string?>((u) => u.UserName)
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
