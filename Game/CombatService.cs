using System.Text.Json;
using Game.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Game;

public class CombatService(IDistributedCache cache,ILogger<CombatService> logger)
{
    private readonly Random _rnd = new();

    public async Task<Monster> CreateBattle()
    {
        var monster =  new Monster(){HP = _rnd.Next(50,200),Damage = _rnd.Next(5,25),Defence = _rnd.Next(0,10)};

        var battleId = "1";
        
        logger.LogInformation("Creating battle");

        await cache.SetAsync(battleId, monster);

        return monster;
    }

    public async Task<Monster?> AttackMonster(string battleId,Character player)
    {
        var data = cache.TryGetValue("1",out Monster? monster);

        if (data)
        {
            player.Attack(monster);
        }
        
        await cache.SetAsync(battleId, monster);

        return monster;
    }
}