using Ardalis.Result;
using MediatR;

namespace Game.Users.Contracts;

public record GetPlayerIdByUserEmailQuery(string Email) : IRequest<Result<Guid>>;