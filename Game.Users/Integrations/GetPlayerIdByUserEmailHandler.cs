using Ardalis.Result;
using Game.Users.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Game.Users.Integrations;

public class GetPlayerIdByUserEmailHandler(UserManager<PlayerIdentity> userManager) : IRequestHandler<GetPlayerIdByUserEmailQuery,Result<Guid>>
{
    public async Task<Result<Guid>> Handle(GetPlayerIdByUserEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Invalid(new ValidationError($"User with email '{request.Email}' not found"));
        }

        return Result.Success(user.PlayerId);
    }
}