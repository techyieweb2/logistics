using MediatR;

namespace TruckService.Api.Features.TruckStatusHistories.Create;

public sealed record CreateTruckStatusHistoryCommand(
    Guid TruckId,
    int Status,
    string? Notes
) : IRequest<CreateTruckStatusHistoryResult>;

public sealed record CreateTruckStatusHistoryResult(Guid Id, Guid TruckId, string Status, DateTime ChangedAt);
