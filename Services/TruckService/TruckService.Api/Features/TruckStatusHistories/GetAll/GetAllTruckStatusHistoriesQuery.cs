using MediatR;

namespace TruckService.Api.Features.TruckStatusHistories.GetAll;

public sealed record GetAllTruckStatusHistoriesQuery(Guid TruckId) : IRequest<IEnumerable<GetAllTruckStatusHistoriesResult>>;

public sealed record GetAllTruckStatusHistoriesResult(
    Guid Id,
    Guid TruckId,
    string Status,
    DateTime ChangedAt,
    string? Notes
);
