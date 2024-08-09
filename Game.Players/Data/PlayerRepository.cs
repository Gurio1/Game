using Ardalis.Result;
using Microsoft.EntityFrameworkCore;

namespace Game.Characters.Data;

public sealed class PlayerRepository : IPlayerRepository
{
    private readonly PlayersDbContext _context;

    public PlayerRepository(PlayersDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result<Player>> GetPlayerByIdAsync(Guid playerId)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);

        if (player is null)
        {
            return Result.Invalid(new ValidationError($"Player with Id '{playerId}' does not exist"));
        }

        return Result.Success(player);
    }

    public async Task<Result> SetBattleIdAsync(Guid playerId, Guid battleId)
    {
        var result = await GetPlayerByIdAsync(playerId);

        if (result.IsInvalid())
        {
            return Result.Invalid(result.ValidationErrors);
        }

        var player = result.Value;

        if (player.BattleId != Guid.Empty)
        {
            return Result.Invalid(new ValidationError($"Player already in battle - Battle Id ={battleId}'"));
        }

        player.BattleId = battleId;

        await SaveChangesAsync();

        return Result.Success();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}