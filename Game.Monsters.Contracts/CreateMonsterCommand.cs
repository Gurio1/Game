using Ardalis.Result;
using MediatR;

namespace Game.Monsters.Contracts;

public record CreateMonsterCommand() : IRequest<Result<CreateMonsterResponse>>;