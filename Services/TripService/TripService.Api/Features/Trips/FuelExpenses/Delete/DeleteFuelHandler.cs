using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.FuelExpenses.Delete;

public sealed class DeleteFuelHandler : IRequestHandler<DeleteFuelCommand, bool>
{
    private readonly TripDbContext _context;

    public DeleteFuelHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteFuelCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return false;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        var fuel = await _context.TripFuelExpenses
            .FirstOrDefaultAsync(f => f.Id == command.FuelId && f.TripId == command.TripId, cancellationToken);

        if (fuel is null)
            return false;

        _context.TripFuelExpenses.Remove(fuel);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
