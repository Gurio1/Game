using Ardalis.Result;
using MediatR;

namespace Game.Players.Contracts;

public record GetPlayerStatsQuery(Guid PlayerId) : IRequest<Result<GetPlayerStatsResponse>>;