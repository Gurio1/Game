using Ardalis.Result;

namespace Game.Characters;

public interface IPlayerRepository
{
    Task<Result<Player>> GetPlayerByIdAsync(Guid playerId);
    Task<Result> SetBattleIdAsync(Guid playerId,Guid battleId);

    Task SaveChangesAsync();
}