using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckStatusHistories.GetAll;

public sealed class GetAllTruckStatusHistoriesHandler : IRequestHandler<GetAllTruckStatusHistoriesQuery, IEnumerable<GetAllTruckStatusHistoriesResult>>
{
    private readonly TruckDbContext _context;

    public GetAllTruckStatusHistoriesHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllTruckStatusHistoriesResult>> Handle(GetAllTruckStatusHistoriesQuery query, CancellationToken cancellationToken)
    {
        return await _context.TruckStatusHistories
            .AsNoTracking()
            .Where(x => x.TruckId == query.TruckId)
            .OrderByDescending(x => x.ChangedAt)
            .Select(x => new GetAllTruckStatusHistoriesResult(
                x.Id,
                x.TruckId,
                x.Status.ToString(),
                x.ChangedAt,
                x.Notes
            ))
            .ToListAsync(cancellationToken);
    }
}
