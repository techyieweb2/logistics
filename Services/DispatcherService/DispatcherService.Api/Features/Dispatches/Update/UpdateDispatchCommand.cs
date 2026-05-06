using MediatR;

namespace DispatcherService.Api.Features.Dispatches.Update;

public sealed record UpdateDispatchCommand(
    Guid Id,
    Guid DriverId,
    Guid TruckId,
    Guid RouteId,
    Guid CustomerId,
    string Status,
    DateTime ScheduledDate,
    DateTime? DepartureDate,
    DateTime? ArrivalDate,
    string? Notes,
    string? StatusRemarks
) : IRequest<UpdateDispatchResult?>;

public sealed record UpdateDispatchResult(
    Guid Id,
    string ReferenceNumber,
    string Status,
    DateTime UpdatedAt
);
