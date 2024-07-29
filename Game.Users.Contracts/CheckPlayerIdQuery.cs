using Ardalis.Result;
using MediatR;

namespace Game.Users.Contracts;

public record CheckPlayerIdQuery(string Email) : IRequest<Result>;