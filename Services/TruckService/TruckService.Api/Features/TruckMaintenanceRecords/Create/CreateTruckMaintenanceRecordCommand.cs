using MediatR;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Create;

public sealed record CreateTruckMaintenanceRecordCommand(
    Guid TruckId,
    string? MaintenanceType,
    string? Description,
    string? OdometerReading,
    DateTime ServiceDate,
    string? ServicedBy,
    decimal? Cost,
    DateTime? NextServiceDate
) : IRequest<CreateTruckMaintenanceRecordResult>;

public sealed record CreateTruckMaintenanceRecordResult(Guid Id, Guid TruckId, string? MaintenanceType, DateTime ServiceDate);
