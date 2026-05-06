using MediatR;

namespace DispatcherService.Api.Features.Dispatches.GetAll;

public sealed record GetAllDispatchesQuery() : IRequest<IEnumerable<GetAllDispatchesResult>>;

public sealed record GetAllDispatchesResult(
    Guid Id,
    string ReferenceNumber,
    Guid DriverId,
    Guid TruckId,
    Guid CustomerId,
    string RouteName,
    string Origin,
    string Destination,
    string Status,
    DateTime ScheduledDate,
    DateTime? DepartureDate,
    DateTime? ArrivalDate,
    DateTime CreatedAt
);
