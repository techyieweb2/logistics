using MediatR;

namespace DispatcherService.Api.Features.Routes.Get;

public sealed record GetRouteQuery(Guid Id) : IRequest<GetRouteResult?>;

public sealed record GetRouteResult(
    Guid Id,
    string Name,
    string Origin,
    string Destination,
    int? EstimatedHours,
    decimal? DistanceKm,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
