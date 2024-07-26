using FastEndpoints;
using FluentValidation;

namespace Game.Users.Endpoints;

public class IsEmailUniqueRequestValidator : Validator<IsEmailUniqueRequest>
{
    public IsEmailUniqueRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required")
            .EmailAddress()
            .WithMessage("Invalid email");
    }
}