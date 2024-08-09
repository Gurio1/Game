using Ardalis.Result;
using Game.Players.Contracts;
using MediatR;
using Serilog;

namespace Game.Characters.Integrations;

public class GetPlayerStatsHandler : IRequestHandler<GetPlayerStatsQuery,Result<GetPlayerStatsResponse>>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ILogger _logger;

    public GetPlayerStatsHandler(IPlayerRepository playerRepository,ILogger logger)
    {
        _playerRepository = playerRepository;
        _logger = logger;
    }
    
    public async Task<Result<GetPlayerStatsResponse>> Handle(GetPlayerStatsQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("Taking player stats with id - {playerId}",request.PlayerId);

        var queryResult = await _playerRepository.GetPlayerByIdAsync(request.PlayerId);

        if (queryResult.IsInvalid())
        {
            return Result.Invalid(queryResult.ValidationErrors);
        }

        var player = queryResult.Value;

        var response = new GetPlayerStatsResponse(player.Strength, player.Endurance, player.HP, player.CurrentHP);

        return Result.Success(response);
    }
}