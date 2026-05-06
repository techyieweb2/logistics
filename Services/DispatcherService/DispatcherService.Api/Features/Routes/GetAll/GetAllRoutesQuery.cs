using MediatR;

namespace DispatcherService.Api.Features.Routes.GetAll;

public sealed record GetAllRoutesQuery() : IRequest<IEnumerable<GetAllRoutesResult>>;

public sealed record GetAllRoutesResult(
    Guid Id,
    string Name,
    string Origin,
    string Destination,
    int? EstimatedHours,
    decimal? DistanceKm,
    bool IsActive,
    DateTime CreatedAt
);
