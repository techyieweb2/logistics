using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.GetAll;

public sealed class GetAllTripsHandler : IRequestHandler<GetAllTripsQuery, IEnumerable<GetAllTripsResult>>
{
    private readonly TripDbContext _context;

    public GetAllTripsHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllTripsResult>> Handle(GetAllTripsQuery query, CancellationToken cancellationToken)
    {
        return await _context.Trips
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new GetAllTripsResult(
                t.Id,
                t.DispatchId,
                t.TruckPlateNumber,
                t.DriverName,
                t.CustomerName,
                t.RouteName,
                t.Origin,
                t.Destination,
                t.FreightCost,
                t.DriverPay,
                t.Status.ToString(),
                t.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}