using MediatR;

namespace TruckService.Api.Features.TruckMaintenanceRecords.GetAll;

public sealed record GetAllTruckMaintenanceRecordsQuery(Guid TruckId) : IRequest<IEnumerable<GetAllTruckMaintenanceRecordsResult>>;

public sealed record GetAllTruckMaintenanceRecordsResult(
    Guid Id,
    Guid TruckId,
    string? MaintenanceType,
    string? OdometerReading,
    DateTime ServiceDate,
    string? ServicedBy,
    decimal? Cost,
    DateTime? NextServiceDate,
    DateTime CreatedAt
);
