using MediatR;

namespace TruckService.Api.Features.TruckTrailers.Get;

public sealed record GetTruckTrailerQuery(Guid TruckId, Guid Id) : IRequest<GetTruckTrailerResult>;

public sealed record GetTruckTrailerResult(
    Guid Id,
    Guid TruckId,
    string TrailerNumber,
    string? TrailerType,
    string? Capacity,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
