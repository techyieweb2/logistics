using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Route = DispatcherService.Domain.Entities.Route;

namespace DispatcherService.Api.Features.Routes.Create;

public sealed class CreateRouteHandler : IRequestHandler<CreateRouteCommand, CreateRouteResult>
{
    private readonly DispatcherDbContext _context;

    public CreateRouteHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<CreateRouteResult> Handle(CreateRouteCommand command, CancellationToken cancellationToken)
    {
        var route = new Route
        {
            Name           = command.Name,
            Origin         = command.Origin,
            Destination    = command.Destination,
            EstimatedHours = command.EstimatedHours,
            DistanceKm     = command.DistanceKm,
            IsActive       = true,
            CreatedAt      = DateTime.UtcNow
        };

        _context.Routes.Add(route);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateRouteResult(
            route.Id,
            route.Name,
            route.Origin,
            route.Destination,
            route.EstimatedHours,
            route.DistanceKm,
            route.IsActive
        );
    }
}
