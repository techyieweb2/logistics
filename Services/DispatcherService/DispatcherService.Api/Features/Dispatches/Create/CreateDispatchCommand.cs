using MediatR;

namespace DispatcherService.Api.Features.Dispatches.Create;

public sealed record CreateDispatchCommand(
    Guid DriverId,
    Guid TruckId,
    Guid RouteId,
    Guid CustomerId,
    DateTime ScheduledDate,
    string? Notes
) : IRequest<CreateDispatchResult>;

public sealed record CreateDispatchResult(
    Guid Id,
    string ReferenceNumber,
    Guid DriverId,
    Guid TruckId,
    Guid RouteId,
    Guid CustomerId,
    string Status,
    DateTime ScheduledDate
);
