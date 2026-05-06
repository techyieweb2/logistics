using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckMaintenanceRecords.GetAll;

public sealed class GetAllTruckMaintenanceRecordsHandler : IRequestHandler<GetAllTruckMaintenanceRecordsQuery, IEnumerable<GetAllTruckMaintenanceRecordsResult>>
{
    private readonly TruckDbContext _context;

    public GetAllTruckMaintenanceRecordsHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllTruckMaintenanceRecordsResult>> Handle(GetAllTruckMaintenanceRecordsQuery query, CancellationToken cancellationToken)
    {
        return await _context.TruckMaintenanceRecords
            .AsNoTracking()
            .Where(x => x.TruckId == query.TruckId)
            .OrderByDescending(x => x.ServiceDate)
            .Select(x => new GetAllTruckMaintenanceRecordsResult(
                x.Id,
                x.TruckId,
                x.MaintenanceType,
                x.OdometerReading,
                x.ServiceDate,
                x.ServicedBy,
                x.Cost,
                x.NextServiceDate,
                x.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}
