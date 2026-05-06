using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Routes.Update;

public sealed class UpdateRouteHandler : IRequestHandler<UpdateRouteCommand, UpdateRouteResult?>
{
    private readonly DispatcherDbContext _context;

    public UpdateRouteHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateRouteResult?> Handle(UpdateRouteCommand command, CancellationToken cancellationToken)
    {
        var route = await _context.Routes
            .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);

        if (route is null)
            return null;

        route.Name           = command.Name;
        route.Origin         = command.Origin;
        route.Destination    = command.Destination;
        route.EstimatedHours = command.EstimatedHours;
        route.DistanceKm     = command.DistanceKm;
        route.IsActive       = command.IsActive;
        route.UpdatedAt      = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateRouteResult(
            route.Id,
            route.Name,
            route.Origin,
            route.Destination,
            route.IsActive,
            route.UpdatedAt!.Value
        );
    }
}
