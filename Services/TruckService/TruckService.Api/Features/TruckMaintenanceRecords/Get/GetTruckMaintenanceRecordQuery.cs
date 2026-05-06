using MediatR;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Get;

public sealed record GetTruckMaintenanceRecordQuery(Guid TruckId, Guid Id) : IRequest<GetTruckMaintenanceRecordResult>;

public sealed record GetTruckMaintenanceRecordResult(
    Guid Id,
    Guid TruckId,
    string? MaintenanceType,
    string? Description,
    string? OdometerReading,
    DateTime ServiceDate,
    string? ServicedBy,
    decimal? Cost,
    DateTime? NextServiceDate,
    DateTime CreatedAt
);
