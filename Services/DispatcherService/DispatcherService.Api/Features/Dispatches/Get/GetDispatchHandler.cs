using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Dispatches.Get;

public sealed class GetDispatchHandler : IRequestHandler<GetDispatchQuery, GetDispatchResult?>
{
    private readonly DispatcherDbContext _context;

    public GetDispatchHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<GetDispatchResult?> Handle(GetDispatchQuery query, CancellationToken cancellationToken)
    {
        var dispatch = await _context.Dispatches
            .AsNoTracking()
            .Include(d => d.Route)
            .Include(d => d.StatusHistories)
            .FirstOrDefaultAsync(d => d.Id == query.Id, cancellationToken);

        if (dispatch is null)
            return null;

        return new GetDispatchResult(
            dispatch.Id,
            dispatch.ReferenceNumber,
            dispatch.DriverId,
            dispatch.TruckId,
            dispatch.RouteId,
            dispatch.CustomerId,
            dispatch.Route.Name,
            dispatch.Route.Origin,
            dispatch.Route.Destination,
            dispatch.Status.ToString(),
            dispatch.ScheduledDate,
            dispatch.DepartureDate,
            dispatch.ArrivalDate,
            dispatch.Notes,
            dispatch.CreatedAt,
            dispatch.UpdatedAt,
            dispatch.StatusHistories
                .OrderByDescending(s => s.ChangedAt)
                .Select(s => new DispatchStatusHistoryResult(
                    s.Status.ToString(),
                    s.ChangedAt,
                    s.Remarks
                ))
        );
    }
}
