using System.Text.RegularExpressions;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Game.Users.Endpoints;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator(UserManager<ApplicationUser> userManager)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required")
            .EmailAddress()
            .WithMessage("Invalid email")
            .MustAsync(async (email, ct) =>
            {
                return await userManager.Users.AnyAsync(u => u.Email != email);
            }).WithMessage("This email is already taken");


        RuleFor(x => x.Password)
            .MinimumLength(8)
            .WithMessage("Password cant be less than 8 symbols")
            .NotEmpty()
            .WithMessage("Password required")
            .Equal(x => x.ConfirmedPassword)
            .WithMessage("Password and Confirmed password should be equal")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*()_+\[\]{}:;<>,.?/~\\-]").WithMessage("Password must contain at least one special symbol.");
    }
}