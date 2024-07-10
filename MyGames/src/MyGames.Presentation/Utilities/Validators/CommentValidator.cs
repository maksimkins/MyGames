using FluentValidation;

namespace MyGames.Presentation.Utilities.Validators;

using MyGames.Core.Comment.Models;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator() 
    {
        RuleFor<string?>((c) => c.Text)
            .NotEmpty()
            .NotNull();

        RuleFor<int?>((c) => c.GameId)
            .NotNull()
            .Must(id => id > 0);
        
        RuleFor<int?>((c) => c.UserId)
            .NotNull()
            .Must(id => id > 0);
    }
}
