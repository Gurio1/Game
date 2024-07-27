using Ardalis.Result;
using Game.Monsters.Contracts;
using MediatR;

namespace Game.Monsters.Integrations;

public class CreateMonsterHandler : IRequestHandler<CreateMonsterCommand,Result<CreateMonsterResponse>>
{
    public Task<Result<CreateMonsterResponse>> Handle(CreateMonsterCommand request, CancellationToken cancellationToken)
    {
        var monster = new Monster(10, 15);
        var monsterData = new CreateMonsterResponse(monster.Strength, monster.Endurance,monster.HP,monster.CurrentHP);

        return Task.FromResult(Result<CreateMonsterResponse>.Success(monsterData));
    }
}