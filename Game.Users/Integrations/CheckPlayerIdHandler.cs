using Ardalis.Result;
using Game.Users.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Game.Users.Integrations;

public class CheckPlayerIdHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<CheckPlayerIdQuery, Result>
{
    public Task<Result> Handle(CheckPlayerIdQuery request, CancellationToken cancellationToken)
    {
        var playerId = userManager.Users.Where(u => u.Email == request.Email).Select(u => u.PlayerId).FirstOrDefault();

        return Task.FromResult(playerId == Guid.Empty ? Result.Success() :
            Result.Invalid(new ValidationError("This user has already created a player")));
    }
}