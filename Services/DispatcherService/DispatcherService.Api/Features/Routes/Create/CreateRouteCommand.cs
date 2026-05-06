using MediatR;

namespace DispatcherService.Api.Features.Routes.Create;

public sealed record CreateRouteCommand(
    string Name,
    string Origin,
    string Destination,
    int? EstimatedHours,
    decimal? DistanceKm
) : IRequest<CreateRouteResult>;

public sealed record CreateRouteResult(
    Guid Id,
    string Name,
    string Origin,
    string Destination,
    int? EstimatedHours,
    decimal? DistanceKm,
    bool IsActive
);
