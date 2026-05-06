using MediatR;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Delete;

public sealed record DeleteTruckMaintenanceRecordCommand(Guid TruckId, Guid Id) : IRequest<DeleteTruckMaintenanceRecordResult>;

public sealed record DeleteTruckMaintenanceRecordResult(Guid Id, string Message);
