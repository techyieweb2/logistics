using MediatR;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.Create;

public sealed class CreateTripHandler : IRequestHandler<CreateTripCommand, CreateTripResult>
{
    private readonly TripDbContext _context;

    public CreateTripHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTripResult> Handle(CreateTripCommand command, CancellationToken cancellationToken)
    {
        var trip = new Trip
        {
            DispatchId = command.DispatchId,

            // Truck snapshot
            TruckId = command.TruckId,
            TruckPlateNumber = command.TruckPlateNumber,

            // Driver snapshot
            DriverId = command.DriverId,
            DriverName = command.DriverName,

            // Customer snapshot
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,

            // Route snapshot
            RouteName = command.RouteName,
            Origin = command.Origin,
            Destination = command.Destination,

            FreightCost = command.FreightCost,
            DriverPay = command.DriverPay,
            Status = TripStatus.Open,
            Notes = command.Notes,
            CreatedAt = DateTime.UtcNow
        };

        // TollFee / Other expenses
        if (command.Expenses is not null)
            foreach (var e in command.Expenses)
                trip.Expenses.Add(new TripExpense
                {
                    TripId = trip.Id,
                    ExpenseType = Enum.Parse<ExpenseType>(e.ExpenseType),
                    Description = e.Description,
                    Amount = e.Amount,
                    CreatedAt = DateTime.UtcNow
                });

        // Helpers
        if (command.Helpers is not null)
            foreach (var h in command.Helpers)
                trip.Helpers.Add(new TripHelper
                {
                    TripId = trip.Id,
                    HelperName = h.HelperName,
                    Pay = h.Pay,
                    CreatedAt = DateTime.UtcNow
                });

        // Fuel — Amount auto-computed
        if (command.FuelExpenses is not null)
            foreach (var f in command.FuelExpenses)
                trip.FuelExpenses.Add(new TripFuelExpense
                {
                    TripId = trip.Id,
                    FuelType = Enum.Parse<FuelType>(f.FuelType),
                    Liters = f.Liters,
                    PricePerLiter = f.PricePerLiter,
                    Amount = f.Liters * f.PricePerLiter,
                    Station = f.Station,
                    CreatedAt = DateTime.UtcNow
                });

        // Spare Parts — TotalCost auto-computed
        if (command.SpareParts is not null)
            foreach (var s in command.SpareParts)
                trip.SpareParts.Add(new TripSparePart
                {
                    TripId = trip.Id,
                    PartName = s.PartName,
                    Quantity = s.Quantity,
                    UnitCost = s.UnitCost,
                    TotalCost = s.Quantity * s.UnitCost,
                    CreatedAt = DateTime.UtcNow
                });

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTripResult(
            trip.Id,
            trip.DispatchId,
            trip.TruckPlateNumber,
            trip.DriverName,
            trip.CustomerName,
            trip.RouteName,
            trip.Origin,
            trip.Destination,
            trip.FreightCost,
            trip.DriverPay,
            trip.Status.ToString()
        );
    }
}