using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using Game.Characters.Data;
using Game.Users.Contracts;
using MediatR;

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
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        
        var query = new CheckPlayerIdQuery(emailAddress);

        var result = await mediator.Send(query, ct);

        if (result.IsInvalid())
        {
            foreach (var error in result.Errors)
            {
                AddError(error);
            }
            ThrowIfAnyErrors();
        }

        var player = new Player(req.UserName, 10, 15);

        dbContext.Players.Add(player);

        var command = new SetPlayerIdCommand(player.Id, emailAddress);

        var commandResult = await mediator.Send(command, ct);

        if (commandResult.IsInvalid())
        {
            foreach (var error in commandResult.Errors)
            {
                AddError(error);
            }
            ThrowIfAnyErrors();
        }

        dbContext.Players.Add(player);
        await dbContext.SaveChangesAsync(ct);
        
        await SendOkAsync(ct);
    }
}