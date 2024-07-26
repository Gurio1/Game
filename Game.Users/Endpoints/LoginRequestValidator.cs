using FastEndpoints;
using FluentValidation;

namespace Game.Users.Endpoints;

public class LoginRequestValidator : Validator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required")
            .EmailAddress()
            .WithMessage("Invalid email");
    }
}