using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using Game.Characters;
using Game.Characters.Data;
using Game.Users.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Game.Characters.Endpoints;

public class Create(IMediator mediator,PlayersDbContext dbContext) : Endpoint<CreateRequest>
{
    public override void Configure()
    {
        Post("/players");
        Claims("EmailAddress");
    }
    
    public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
    {
        var isUserNameNotUnique =  await dbContext.Players.AnyAsync(p => p.UserName == req.UserName, cancellationToken: ct);
        if (isUserNameNotUnique)
        {
            ThrowError(request => request.UserName, "This UserName is already taken");
        }
        
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        
        var query = new CheckPlayerIdQuery(emailAddress);

        var result = await mediator.Send(query, ct);

        if (result.IsInvalid())
        {
            foreach (var error in result.ValidationErrors)
            {
                AddError(error.ErrorMessage,error.ErrorCode);
            }
            ThrowIfAnyErrors();
        }

        var player = new Player(req.UserName, 10, 15);

        dbContext.Players.Add(player);

        var command = new SetPlayerIdCommand(player.Id, emailAddress);

        var commandResult = await mediator.Send(command, ct);

        if (commandResult.IsInvalid())
        {
            foreach (var error in commandResult.ValidationErrors)
            {
                AddError(error.ErrorMessage,error.ErrorMessage);
            }
            ThrowIfAnyErrors();
        }

        dbContext.Players.Add(player);
        await dbContext.SaveChangesAsync(ct);
        
        //TO DO: Create ViewModel for this
        await SendOkAsync(player,ct);
    }
}