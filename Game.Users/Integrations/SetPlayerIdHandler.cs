using Ardalis.Result;
using Game.Users.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Game.Users.Integrations;

public class SetPlayerIdHandler(UserManager<PlayerIdentity> userManager,ILogger logger) : IRequestHandler<SetPlayerIdCommand,Result>
{
    public async Task<Result> Handle(SetPlayerIdCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Setting player id  - {id} to the user with email - {email}",
            request.PlayerId,request.Email);
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Invalid(new ValidationError($"User with email {request.Email} does not exist"));
        }

        if (user.PlayerId != Guid.Empty)
        {
            return Result.Invalid(new ValidationError($"User already have a player with id : {user.PlayerId}"));
        }

        user.PlayerId = request.PlayerId;

        await userManager.UpdateAsync(user);

        return Result.Success();
    }
}