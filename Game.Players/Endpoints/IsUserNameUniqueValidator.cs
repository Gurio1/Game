using FastEndpoints;
using FluentValidation;

namespace Game.Characters.Endpoints;

public class IsUserNameUniqueValidator : Validator<IsUserNameUniqueRequest>
{
    public IsUserNameUniqueValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("User name is required")
            .MinimumLength(4)
            .WithMessage("Minimum length is 4 symbols")
            .Matches("/^[a-zA-Z0-9]*$/")
            .WithMessage("User name can contain only latin and numeric values");
    }
}