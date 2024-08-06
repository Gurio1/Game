using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using Game.Users.Contracts;
using MediatR;

namespace Game.Characters.Endpoints;

public class Get : EndpointWithoutRequest<Player>
{
    private readonly IMediator _mediator;
    private readonly IPlayerRepository _playerRepository;

    public Get(IMediator mediator,IPlayerRepository playerRepository)
    {
        _mediator = mediator;
        _playerRepository = playerRepository;
    }
    
    public override void Configure()
    {
        Get("/players");
        Claims("EmailAddress");
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;

        var query = new GetPlayerIdByUserEmailQuery(emailAddress);

        var result = await _mediator.Send(query, ct);

        if (result.IsInvalid())
        {
            foreach (var validationError in result.ValidationErrors)
            {
                AddError(validationError.ErrorMessage);
            }
            
            ThrowIfAnyErrors();
        }

        var playerId = result.Value;

        if (playerId == Guid.Empty)
        {
            ThrowError("Player id can not be empty.Please save request and contact administrator",500);
        }

        var creationResult = await _playerRepository.GetPlayerById(playerId);

        if (result.IsInvalid())
        {
            foreach (var validationError in creationResult.ValidationErrors)
            {
                AddError(validationError.ErrorMessage);
            }
            
            ThrowIfAnyErrors();
        }

        await SendOkAsync(creationResult.Value, ct);

    }
}