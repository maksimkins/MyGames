using FluentValidation;

namespace MyGames.Presentation.Utilities.Validators;

using MyGames.Core.Dtos.Models;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor<string?>((u) => u.Username)     
            .NotEmpty().WithMessage("Your login cannot be empty.");
            
        RuleFor<string?>((u) => u.Password)
            .NotEmpty().WithMessage("Your password cannot be empty."); 
    }
}
