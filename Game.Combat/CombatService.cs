using Game.Combat.UseCases;
using Game.Monsters.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Game.Combat;

public class CombatService(IDistributedCache cache,ILogger<CombatService> logger,IMediator mediator)
{
    public async Task<Battle> CreateBattle()
    {
        var query = new GetMonsterQuery();

        var result = await mediator.Send(query);

        var monsterData = result.Value;
        
        logger.LogInformation("Creating battle");

        var battle = new Battle(new Monster(monsterData.Strength,monsterData.Endurance,monsterData.Hp));

        await cache.SetAsync(battle.Id,battle);

        return battle;
    }

    /*public async Task<Monster?> AttackMonster(string battleId,Character player)
    {
        var data = cache.TryGetValue("1",out object? monster);

        if (data)
        {
            player.Attack(monster);
        }
        
        await cache.SetAsync(battleId, monster);

        return monster;
    }*/
}