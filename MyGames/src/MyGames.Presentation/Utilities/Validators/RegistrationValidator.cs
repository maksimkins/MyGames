using FluentValidation;

namespace MyGames.Presentation.Utilities.Validators;

using MyGames.Core.Dtos.Models;

public class RegistrationValidator : AbstractValidator<RegistrationDto>
{
    public RegistrationValidator()
    {
        RuleFor<DateTime?>((u) => u.Birthdate)
            .NotEmpty().WithMessage("Your birthdate cannot be empty.");
    
        RuleFor<string?>((u) => u.Email)
            .NotEmpty().WithMessage("Your email cannot be empty.")
            .EmailAddress().WithMessage("Your have to write your email.");

        RuleFor<string?>((u) => u.Username)
            .NotEmpty().WithMessage("Your username cannot be empty.")
            .MinimumLength(5).WithMessage("Your password length must be at least 5.")
            .MaximumLength(18).WithMessage("Your username length must not exceed 18.");

        RuleFor<string?>((u) => u.Password)
            .NotEmpty().WithMessage("Your password cannot be empty.")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
    }
}
