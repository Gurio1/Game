using Ardalis.Result;
using Game.Monsters.Contracts;
using MediatR;
using Serilog;

namespace Game.Monsters.Integrations;

public class CreateMonsterHandler(ILogger logger) : IRequestHandler<CreateMonsterCommand,Result<CreateMonsterResponse>>
{
    public Task<Result<CreateMonsterResponse>> Handle(CreateMonsterCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Creating a monster");
        var monster = new Monster(10, 15);
        var monsterData = new CreateMonsterResponse(monster.Strength, monster.Endurance,monster.HP,monster.CurrentHP);

        return Task.FromResult(Result<CreateMonsterResponse>.Success(monsterData));
    }
}