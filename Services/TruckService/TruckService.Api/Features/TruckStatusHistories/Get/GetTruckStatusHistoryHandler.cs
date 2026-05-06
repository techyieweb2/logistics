using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckStatusHistories.Get;

public sealed class GetTruckStatusHistoryHandler : IRequestHandler<GetTruckStatusHistoryQuery, GetTruckStatusHistoryResult>
{
    private readonly TruckDbContext _context;

    public GetTruckStatusHistoryHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<GetTruckStatusHistoryResult> Handle(GetTruckStatusHistoryQuery query, CancellationToken cancellationToken)
    {
        var history = await _context.TruckStatusHistories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id && x.TruckId == query.TruckId, cancellationToken);

        if (history is null)
            throw new KeyNotFoundException($"Status history with ID '{query.Id}' was not found for Truck '{query.TruckId}'.");

        return new GetTruckStatusHistoryResult(
            history.Id,
            history.TruckId,
            history.Status.ToString(),
            history.ChangedAt,
            history.Notes
        );
    }
}
