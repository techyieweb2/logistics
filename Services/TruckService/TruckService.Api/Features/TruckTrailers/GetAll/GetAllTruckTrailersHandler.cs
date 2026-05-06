using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckTrailers.GetAll;

public sealed class GetAllTruckTrailersHandler : IRequestHandler<GetAllTruckTrailersQuery, IEnumerable<GetAllTruckTrailersResult>>
{
    private readonly TruckDbContext _context;

    public GetAllTruckTrailersHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllTruckTrailersResult>> Handle(GetAllTruckTrailersQuery query, CancellationToken cancellationToken)
    {
        return await _context.TruckTrailers
            .AsNoTracking()
            .Where(x => x.TruckId == query.TruckId)
            .OrderBy(x => x.TrailerNumber)
            .Select(x => new GetAllTruckTrailersResult(
                x.Id,
                x.TruckId,
                x.TrailerNumber,
                x.TrailerType,
                x.Capacity,
                x.Status.ToString(),
                x.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}
