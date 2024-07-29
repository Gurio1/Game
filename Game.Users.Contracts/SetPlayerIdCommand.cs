using Ardalis.Result;
using MediatR;

namespace Game.Users.Contracts;

public record SetPlayerIdCommand(Guid PlayerId,string Email) : IRequest<Result>;