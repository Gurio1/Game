using Ardalis.Result;

namespace Game.Characters;

public interface IPlayerRepository
{
    Task<Result<Player>> GetPlayerById(Guid playerId);

    Task SaveChangesAsync();
}