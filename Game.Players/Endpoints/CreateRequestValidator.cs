using FastEndpoints;
using FluentValidation;
using Game.Characters.Data;
using Microsoft.EntityFrameworkCore;

namespace Game.Characters.Endpoints;

public class CreateRequestValidator : Validator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required")
            .MinimumLength(4)
            .WithMessage("UserName cant be less than 4 symbols");
    }
}