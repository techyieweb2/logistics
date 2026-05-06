using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Delete;

public sealed class DeleteTruckMaintenanceRecordHandler : IRequestHandler<DeleteTruckMaintenanceRecordCommand, DeleteTruckMaintenanceRecordResult>
{
    private readonly TruckDbContext _context;

    public DeleteTruckMaintenanceRecordHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteTruckMaintenanceRecordResult> Handle(DeleteTruckMaintenanceRecordCommand command, CancellationToken cancellationToken)
    {
        var record = await _context.TruckMaintenanceRecords
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.TruckId == command.TruckId, cancellationToken);

        if (record is null)
            throw new KeyNotFoundException($"Maintenance record with ID '{command.Id}' was not found for Truck '{command.TruckId}'.");

        _context.TruckMaintenanceRecords.Remove(record);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTruckMaintenanceRecordResult(record.Id, $"Maintenance record has been deleted successfully.");
    }
}
