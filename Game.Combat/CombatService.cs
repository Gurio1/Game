using Ardalis.Result;
using Game.Combat.UseCases;
using Game.Players.Contracts;
using Game.Users.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

namespace Game.Combat;

public class CombatService(IDistributedCache cache,ILogger logger,IMediator mediator)
{
    private static Guid BattleId = Guid.Empty;
    public async Task<Battle> CreateBattle(string userEmail)
    {
        var query = new GetMonsterQuery();
        var result = await mediator.Send(query);
        var monsterData = result.Value;
        
        //need to figure out how to put playerId into token
        var getPlayerIdQuery = new GetPlayerIdByUserEmailQuery(userEmail);
        var getPlayerIdQueryResult = await mediator.Send(getPlayerIdQuery);
        
        if (getPlayerIdQueryResult.IsInvalid())
        {
            foreach (var error in getPlayerIdQueryResult.ValidationErrors)
            {
                logger.Warning("Validation error during getting the player id - {error}",error.ErrorMessage);
            }
        }

        var getPlayerStatsQuery = new GetPlayerStatsQuery(getPlayerIdQueryResult.Value);
        var getPlayerStatsQueryResult = await mediator.Send(getPlayerStatsQuery);
        var playerStats = getPlayerStatsQueryResult.Value;

        var player = new Player(playerStats.Strength, playerStats.Endurance, playerStats.HP, playerStats.CurrentHP);
        
        logger.Information("Creating a battle");
        var battle = new Battle(new Monster(monsterData.Strength,monsterData.Endurance,monsterData.Hp),player);

        BattleId = battle.Id;
        
        var setBattleIdCommand = new SetBattleIdCommand(getPlayerIdQueryResult.Value,battle.Id);
        var commandResult = await mediator.Send(setBattleIdCommand);

        if (commandResult.IsInvalid())
        {
            foreach (var error in commandResult.ValidationErrors)
            {
                logger.Warning("Validation error during setting the battle id - {error}",error.ErrorMessage);
            }
        }

        logger.Information("Setting the battle to the cache");
        await cache.SetAsync(battle.Id.ToString(),battle);

        return battle;
    }

    public Task AttackMonster(string userEmail)
    {
        var data = cache.TryGetValue(BattleId.ToString(),out object? monster);
        return Task.CompletedTask;

        //await cache.SetAsync(battleId, monster);
    }
}