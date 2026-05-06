using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Routes.GetAll;

public sealed class GetAllRoutesHandler : IRequestHandler<GetAllRoutesQuery, IEnumerable<GetAllRoutesResult>>
{
    private readonly DispatcherDbContext _context;

    public GetAllRoutesHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllRoutesResult>> Handle(GetAllRoutesQuery query, CancellationToken cancellationToken)
    {
        return await _context.Routes
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .Select(r => new GetAllRoutesResult(
                r.Id,
                r.Name,
                r.Origin,
                r.Destination,
                r.EstimatedHours,
                r.DistanceKm,
                r.IsActive,
                r.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}
