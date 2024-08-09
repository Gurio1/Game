using Ardalis.Result;
using Game.Players.Contracts;
using MediatR;
using Serilog;

namespace Game.Characters.Integrations;

public sealed class SetBattleIdHandler : IRequestHandler<SetBattleIdCommand,Result>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ILogger _logger;

    public SetBattleIdHandler(IPlayerRepository playerRepository,ILogger logger)
    {
        _playerRepository = playerRepository;
        _logger = logger;
    }
    public async Task<Result> Handle(SetBattleIdCommand request, CancellationToken cancellationToken)
    {
        _logger.Information("Setting battleId - {battleId} to the player with id - {playerId}",
            request.BattleId,request.PlayerId);
        return await _playerRepository.SetBattleIdAsync(request.PlayerId, request.BattleId);
    }
}