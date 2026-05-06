using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Dispatches.GetAll;

public sealed class GetAllDispatchesHandler : IRequestHandler<GetAllDispatchesQuery, IEnumerable<GetAllDispatchesResult>>
{
    private readonly DispatcherDbContext _context;

    public GetAllDispatchesHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllDispatchesResult>> Handle(GetAllDispatchesQuery query, CancellationToken cancellationToken)
    {
        return await _context.Dispatches
            .AsNoTracking()
            .Include(d => d.Route)
            .OrderByDescending(d => d.CreatedAt)
            .Select(d => new GetAllDispatchesResult(
                d.Id,
                d.ReferenceNumber,
                d.DriverId,
                d.TruckId,
                d.CustomerId,
                d.Route.Name,
                d.Route.Origin,
                d.Route.Destination,
                d.Status.ToString(),
                d.ScheduledDate,
                d.DepartureDate,
                d.ArrivalDate,
                d.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}
