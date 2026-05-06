using MediatR;

namespace DispatcherService.Api.Features.Routes.Update;

public sealed record UpdateRouteCommand(
    Guid Id,
    string Name,
    string Origin,
    string Destination,
    int? EstimatedHours,
    decimal? DistanceKm,
    bool IsActive
) : IRequest<UpdateRouteResult?>;

public sealed record UpdateRouteResult(
    Guid Id,
    string Name,
    string Origin,
    string Destination,
    bool IsActive,
    DateTime UpdatedAt
);
