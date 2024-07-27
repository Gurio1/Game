using Ardalis.Result;
using Game.Monsters.Contracts;
using MediatR;

namespace Game.Combat.UseCases;

public record GetMonsterQuery : IRequest<Result<CreateMonsterResponse>>;