using MediatR;

namespace TruckService.Api.Features.TruckStatusHistories.Get;

public sealed record GetTruckStatusHistoryQuery(Guid TruckId, Guid Id) : IRequest<GetTruckStatusHistoryResult>;

public sealed record GetTruckStatusHistoryResult(
    Guid Id,
    Guid TruckId,
    string Status,
    DateTime ChangedAt,
    string? Notes
);
