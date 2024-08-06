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
    
    public async Task<Result<Player>> GetPlayerById(Guid playerId)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);

        if (player is null)
        {
            return Result.Invalid(new ValidationError($"Player with Id '{playerId}' does not exist"));
        }

        return Result.Success(player);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}