using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Get;

public sealed class GetTruckMaintenanceRecordHandler : IRequestHandler<GetTruckMaintenanceRecordQuery, GetTruckMaintenanceRecordResult>
{
    private readonly TruckDbContext _context;

    public GetTruckMaintenanceRecordHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<GetTruckMaintenanceRecordResult> Handle(GetTruckMaintenanceRecordQuery query, CancellationToken cancellationToken)
    {
        var record = await _context.TruckMaintenanceRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id && x.TruckId == query.TruckId, cancellationToken);

        if (record is null)
            throw new KeyNotFoundException($"Maintenance record with ID '{query.Id}' was not found for Truck '{query.TruckId}'.");

        return new GetTruckMaintenanceRecordResult(
            record.Id,
            record.TruckId,
            record.MaintenanceType,
            record.Description,
            record.OdometerReading,
            record.ServiceDate,
            record.ServicedBy,
            record.Cost,
            record.NextServiceDate,
            record.CreatedAt
        );
    }
}
