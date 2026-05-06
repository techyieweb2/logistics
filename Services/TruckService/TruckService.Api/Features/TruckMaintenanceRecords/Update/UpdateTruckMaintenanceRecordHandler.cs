using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Update;

public sealed class UpdateTruckMaintenanceRecordHandler : IRequestHandler<UpdateTruckMaintenanceRecordCommand, UpdateTruckMaintenanceRecordResult>
{
    private readonly TruckDbContext _context;

    public UpdateTruckMaintenanceRecordHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateTruckMaintenanceRecordResult> Handle(UpdateTruckMaintenanceRecordCommand command, CancellationToken cancellationToken)
    {
        var record = await _context.TruckMaintenanceRecords
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.TruckId == command.TruckId, cancellationToken);

        if (record is null)
            throw new KeyNotFoundException($"Maintenance record with ID '{command.Id}' was not found for Truck '{command.TruckId}'.");

        record.MaintenanceType = command.MaintenanceType;
        record.Description     = command.Description;
        record.OdometerReading = command.OdometerReading;
        record.ServiceDate     = command.ServiceDate;
        record.ServicedBy      = command.ServicedBy;
        record.Cost            = command.Cost;
        record.NextServiceDate = command.NextServiceDate;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateTruckMaintenanceRecordResult(record.Id, record.TruckId, record.MaintenanceType, record.ServiceDate);
    }
}
