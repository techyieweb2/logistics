using MediatR;

namespace DispatcherService.Api.Features.Dispatches.Get;

public sealed record GetDispatchQuery(Guid Id) : IRequest<GetDispatchResult?>;

public sealed record GetDispatchResult(
    Guid Id,
    string ReferenceNumber,
    Guid DriverId,
    Guid TruckId,
    Guid RouteId,
    Guid CustomerId,
    string RouteName,
    string Origin,
    string Destination,
    string Status,
    DateTime ScheduledDate,
    DateTime? DepartureDate,
    DateTime? ArrivalDate,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<DispatchStatusHistoryResult> StatusHistories
);

public sealed record DispatchStatusHistoryResult(
    string Status,
    DateTime ChangedAt,
    string? Remarks
);
