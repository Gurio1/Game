using Ardalis.Result;
using Game.Monsters.Contracts;
using MediatR;

namespace Game.Combat.UseCases;

public class GetMonsterQueryHandler(IMediator mediator) : IRequestHandler<GetMonsterQuery,Result<CreateMonsterResponse>>
{
    public async Task<Result<CreateMonsterResponse>> Handle(GetMonsterQuery request, CancellationToken cancellationToken)
    {
        var command = new CreateMonsterCommand();

        var result = await mediator.Send(command, cancellationToken);

        return !result.IsSuccess ? Result.Invalid() : result;
    }
}