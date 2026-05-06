using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.Trucks.GetAll;

public sealed class GetAllTrucksHandler : IRequestHandler<GetAllTrucksQuery, IEnumerable<GetAllTrucksResult>>
{
    private readonly TruckDbContext _context;

    public GetAllTrucksHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllTrucksResult>> Handle(GetAllTrucksQuery query, CancellationToken cancellationToken)
    {
        return await _context.Trucks
            .AsNoTracking()
            .Select(truck => new GetAllTrucksResult(
                truck.Id,
                truck.PlateNumber,
                truck.Brand,
                truck.Model,
                truck.YearModel,
                truck.TruckType,
                truck.Status.ToString(),
                truck.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}
