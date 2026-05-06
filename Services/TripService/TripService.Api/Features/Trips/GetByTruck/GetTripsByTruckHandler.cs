using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.GetByTruck;

public sealed class GetTripsByTruckHandler : IRequestHandler<GetTripsByTruckQuery, GetTripsByTruckResult>
{
    private readonly TripDbContext _context;

    public GetTripsByTruckHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<GetTripsByTruckResult> Handle(GetTripsByTruckQuery query, CancellationToken cancellationToken)
    {
        var trips = await _context.Trips
            .AsNoTracking()
            .Include(t => t.Expenses)
            .Include(t => t.Helpers)
            .Include(t => t.FuelExpenses)
            .Include(t => t.SpareParts)
            .Where(t =>
                t.TruckId == query.TruckId &&
                t.CreatedAt.Month == query.Month &&
                t.CreatedAt.Year == query.Year)
            .OrderBy(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        var plateNumber = trips.FirstOrDefault()?.TruckPlateNumber ?? string.Empty;

        var tripItems = trips.Select(t => new TruckTripItem(
            t.Id,
            t.DispatchId,
            t.DriverName,
            t.CustomerName,
            t.RouteName,
            t.Origin,
            t.Destination,
            t.FreightCost,
            t.DriverPay,
            t.Status.ToString(),
            t.CreatedAt,
            t.Expenses.Select(e => new TruckTripExpenseItem(
                e.ExpenseType.ToString(), e.Description, e.Amount)),
            t.Helpers.Select(h => new TruckTripHelperItem(
                h.HelperName, h.Pay)),
            t.FuelExpenses.Select(f => new TruckTripFuelItem(
                f.FuelType.ToString(), f.Liters, f.PricePerLiter, f.Amount, f.Station)),
            t.SpareParts.Select(s => new TruckTripSparePartItem(
                s.PartName, s.Quantity, s.UnitCost, s.TotalCost))
        ));

        return new GetTripsByTruckResult(
            query.TruckId,
            plateNumber,
            query.Month,
            query.Year,
            trips.Count,
            tripItems
        );
    }
}