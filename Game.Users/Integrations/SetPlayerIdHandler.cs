using Ardalis.Result;
using Game.Users.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Game.Users.Integrations;

public class SetPlayerIdHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<SetPlayerIdCommand,Result>
{
    public async Task<Result> Handle(SetPlayerIdCommand request, CancellationToken cancellationToken)
    {
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