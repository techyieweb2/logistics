using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Domain.Entities;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Create;

public sealed class CreateTruckMaintenanceRecordHandler : IRequestHandler<CreateTruckMaintenanceRecordCommand, CreateTruckMaintenanceRecordResult>
{
    private readonly TruckDbContext _context;

    public CreateTruckMaintenanceRecordHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTruckMaintenanceRecordResult> Handle(CreateTruckMaintenanceRecordCommand command, CancellationToken cancellationToken)
    {
        var truckExists = await _context.Trucks
            .AnyAsync(x => x.Id == command.TruckId, cancellationToken);

        if (!truckExists)
            throw new KeyNotFoundException($"Truck with ID '{command.TruckId}' was not found.");

        var record = new TruckMaintenanceRecord
        {
            TruckId         = command.TruckId,
            MaintenanceType = command.MaintenanceType,
            Description     = command.Description,
            OdometerReading = command.OdometerReading,
            ServiceDate     = command.ServiceDate,
            ServicedBy      = command.ServicedBy,
            Cost            = command.Cost,
            NextServiceDate = command.NextServiceDate,
            CreatedAt       = DateTime.UtcNow
        };

        _context.TruckMaintenanceRecords.Add(record);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTruckMaintenanceRecordResult(record.Id, record.TruckId, record.MaintenanceType, record.ServiceDate);
    }
}
