using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.Trucks.Get;

public sealed class GetTruckHandler : IRequestHandler<GetTruckQuery, GetTruckResult>
{
    private readonly TruckDbContext _context;

    public GetTruckHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<GetTruckResult> Handle(GetTruckQuery query, CancellationToken cancellationToken)
    {
        var truck = await _context.Trucks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (truck is null)
            throw new KeyNotFoundException($"Truck with ID '{query.Id}' was not found.");

        return new GetTruckResult(
            truck.Id,
            truck.PlateNumber,
            truck.ChassisNumber,
            truck.EngineNumber,
            truck.Brand,
            truck.Model,
            truck.YearModel,
            truck.Color,
            truck.TruckType,
            truck.GrossVehicleWeight,
            truck.Capacity,
            truck.FuelType,
            truck.Status.ToString(),
            truck.AcquisitionDate,
            truck.CreatedAt,
            truck.UpdatedAt
        );
    }
}
