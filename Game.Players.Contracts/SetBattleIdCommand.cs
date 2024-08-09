using Ardalis.Result;
using MediatR;

namespace Game.Players.Contracts;

public record SetBattleIdCommand(Guid PlayerId,Guid BattleId) : IRequest<Result>;