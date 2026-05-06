using MediatR;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Update;

public sealed record UpdateTruckMaintenanceRecordCommand(
    Guid Id,
    Guid TruckId,
    string? MaintenanceType,
    string? Description,
    string? OdometerReading,
    DateTime ServiceDate,
    string? ServicedBy,
    decimal? Cost,
    DateTime? NextServiceDate
) : IRequest<UpdateTruckMaintenanceRecordResult>;

public sealed record UpdateTruckMaintenanceRecordResult(Guid Id, Guid TruckId, string? MaintenanceType, DateTime ServiceDate);
