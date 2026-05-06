using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Routes.Delete;

public sealed class DeleteRouteHandler : IRequestHandler<DeleteRouteCommand, bool>
{
    private readonly DispatcherDbContext _context;

    public DeleteRouteHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteRouteCommand command, CancellationToken cancellationToken)
    {
        var route = await _context.Routes
            .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);

        if (route is null)
            return false;

        // Check if route has active dispatches
        var hasActiveDispatches = await _context.Dispatches
            .AnyAsync(d => d.RouteId == command.Id &&
                           d.Status != DispatcherService.Domain.Enums.DispatchStatus.Completed &&
                           d.Status != DispatcherService.Domain.Enums.DispatchStatus.Cancelled,
                     cancellationToken);

        if (hasActiveDispatches)
            throw new InvalidOperationException("Cannot delete a route that has active dispatches.");

        _context.Routes.Remove(route);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
