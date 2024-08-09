using Ardalis.Result;
using Game.Users.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Game.Users.Integrations;

public class CheckPlayerIdHandler(UserManager<PlayerIdentity> userManager,ILogger logger)
    : IRequestHandler<CheckPlayerIdQuery, Result>
{
    public Task<Result> Handle(CheckPlayerIdQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Checking player id by email - {email}",request.Email);
        var playerId = userManager.Users.Where(u => u.Email == request.Email).Select(u => u.PlayerId).FirstOrDefault();

        return Task.FromResult(playerId == Guid.Empty ? Result.Success() :
            Result.Invalid(new ValidationError("This user has already created a player")));
    }
}