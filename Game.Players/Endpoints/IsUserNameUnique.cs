using FastEndpoints;
using Game.Characters.Data;
using Microsoft.EntityFrameworkCore;

namespace Game.Characters.Endpoints;

public class IsUserNameUnique(PlayersDbContext dbContext) : Endpoint<Player>
{
    
    public override void Configure()
    {
        Post("/players/check-userName");
        Claims("EmailAddress");
    }
    
    public override async Task HandleAsync(Player req, CancellationToken ct)
    {
        var isNotUnique =  await dbContext.Players.AnyAsync(p => p.UserName == req.UserName, cancellationToken: ct);
        
        await SendOkAsync(isNotUnique, ct);
    }

    
}