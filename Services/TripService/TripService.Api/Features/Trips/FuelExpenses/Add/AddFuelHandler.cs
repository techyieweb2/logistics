using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.FuelExpenses.Add;

public sealed class AddFuelHandler : IRequestHandler<AddFuelCommand, AddFuelResult?>
{
    private readonly TripDbContext _context;

    public AddFuelHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<AddFuelResult?> Handle(AddFuelCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return null;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        var fuel = new TripFuelExpense
        {
            TripId        = command.TripId,
            FuelType      = Enum.Parse<FuelType>(command.FuelType),
            Liters        = command.Liters,
            PricePerLiter = command.PricePerLiter,
            Amount        = command.Liters * command.PricePerLiter, // auto-computed
            Station       = command.Station,
            CreatedAt     = DateTime.UtcNow
        };

        _context.TripFuelExpenses.Add(fuel);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddFuelResult(
            fuel.Id,
            fuel.TripId,
            fuel.FuelType.ToString(),
            fuel.Liters,
            fuel.PricePerLiter,
            fuel.Amount,
            fuel.Station,
            fuel.CreatedAt
        );
    }
}
