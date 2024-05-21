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
        
        RuleFor<string?>((c) => c.Title)
            .NotEmpty()
            .NotNull();

        RuleFor<int?>((c) => c.GameId)
            .NotNull();
    }
}
