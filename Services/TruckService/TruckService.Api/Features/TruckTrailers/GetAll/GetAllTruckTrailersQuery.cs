using MediatR;

namespace TruckService.Api.Features.TruckTrailers.GetAll;

public sealed record GetAllTruckTrailersQuery(Guid TruckId) : IRequest<IEnumerable<GetAllTruckTrailersResult>>;

public sealed record GetAllTruckTrailersResult(
    Guid Id,
    Guid TruckId,
    string TrailerNumber,
    string? TrailerType,
    string? Capacity,
    string Status,
    DateTime CreatedAt
);
