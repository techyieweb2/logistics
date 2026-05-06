using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckTrailers.Get;

public sealed class GetTruckTrailerHandler : IRequestHandler<GetTruckTrailerQuery, GetTruckTrailerResult>
{
    private readonly TruckDbContext _context;

    public GetTruckTrailerHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<GetTruckTrailerResult> Handle(GetTruckTrailerQuery query, CancellationToken cancellationToken)
    {
        var trailer = await _context.TruckTrailers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id && x.TruckId == query.TruckId, cancellationToken);

        if (trailer is null)
            throw new KeyNotFoundException($"Trailer with ID '{query.Id}' was not found for Truck '{query.TruckId}'.");

        return new GetTruckTrailerResult(
            trailer.Id,
            trailer.TruckId,
            trailer.TrailerNumber,
            trailer.TrailerType,
            trailer.Capacity,
            trailer.Status.ToString(),
            trailer.CreatedAt,
            trailer.UpdatedAt
        );
    }
}
