using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.SpareParts.Delete;

public sealed class DeleteSparePartHandler : IRequestHandler<DeleteSparePartCommand, bool>
{
    private readonly TripDbContext _context;

    public DeleteSparePartHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteSparePartCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return false;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        var sparePart = await _context.TripSpareParts
            .FirstOrDefaultAsync(s => s.Id == command.SparePartId && s.TripId == command.TripId, cancellationToken);

        if (sparePart is null)
            return false;

        _context.TripSpareParts.Remove(sparePart);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
