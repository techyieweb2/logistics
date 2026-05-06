using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.Get;

public sealed class GetTripHandler : IRequestHandler<GetTripQuery, GetTripResult?>
{
    private readonly TripDbContext _context;

    public GetTripHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<GetTripResult?> Handle(GetTripQuery query, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .AsNoTracking()
            .Include(t => t.Expenses)
            .Include(t => t.Helpers)
            .Include(t => t.FuelExpenses)
            .Include(t => t.SpareParts)
            .FirstOrDefaultAsync(t => t.Id == query.Id, cancellationToken);

        if (trip is null)
            return null;

        return new GetTripResult(
            trip.Id,
            trip.DispatchId,
            trip.TruckId,
            trip.TruckPlateNumber,
            trip.DriverId,
            trip.DriverName,
            trip.CustomerId,
            trip.CustomerName,
            trip.RouteName,
            trip.Origin,
            trip.Destination,
            trip.FreightCost,
            trip.DriverPay,
            trip.Status.ToString(),
            trip.Notes,
            trip.CreatedAt,
            trip.UpdatedAt,
            trip.Expenses.Select(e => new TripExpenseResult(
                e.Id, e.ExpenseType.ToString(), e.Description, e.Amount)),
            trip.Helpers.Select(h => new TripHelperResult(
                h.Id, h.HelperName, h.Pay)),
            trip.FuelExpenses.Select(f => new TripFuelResult(
                f.Id, f.FuelType.ToString(), f.Liters, f.PricePerLiter, f.Amount, f.Station)),
            trip.SpareParts.Select(s => new TripSparePartResult(
                s.Id, s.PartName, s.Quantity, s.UnitCost, s.TotalCost))
        );
    }
}