using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.UpdateDriverPay;

public sealed class UpdateDriverPayHandler : IRequestHandler<UpdateDriverPayCommand, UpdateDriverPayResult?>
{
    private readonly TripDbContext _context;

    public UpdateDriverPayHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateDriverPayResult?> Handle(UpdateDriverPayCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return null;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        trip.DriverPay = command.DriverPay;
        trip.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateDriverPayResult(trip.Id, trip.DriverPay, trip.UpdatedAt!.Value);
    }
}
