using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Routes.Get;

public sealed class GetRouteHandler : IRequestHandler<GetRouteQuery, GetRouteResult?>
{
    private readonly DispatcherDbContext _context;

    public GetRouteHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<GetRouteResult?> Handle(GetRouteQuery query, CancellationToken cancellationToken)
    {
        var route = await _context.Routes
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == query.Id, cancellationToken);

        if (route is null)
            return null;

        return new GetRouteResult(
            route.Id,
            route.Name,
            route.Origin,
            route.Destination,
            route.EstimatedHours,
            route.DistanceKm,
            route.IsActive,
            route.CreatedAt,
            route.UpdatedAt
        );
    }
}
