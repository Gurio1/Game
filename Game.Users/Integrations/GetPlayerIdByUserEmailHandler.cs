using Ardalis.Result;
using Game.Users.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Game.Users.Integrations;

public class GetPlayerIdByUserEmailHandler(UserManager<PlayerIdentity> userManager,ILogger logger) : IRequestHandler<GetPlayerIdByUserEmailQuery,Result<Guid>>
{
    public async Task<Result<Guid>> Handle(GetPlayerIdByUserEmailQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Getting player id by email - {email}",request.Email);

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Invalid(new ValidationError($"User with email '{request.Email}' not found"));
        }

        return Result.Success(user.PlayerId);
    }
}