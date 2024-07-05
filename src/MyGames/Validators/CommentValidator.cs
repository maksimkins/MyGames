using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MyGames.Models;

namespace MyGames.Validators;

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
