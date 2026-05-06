using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.SpareParts.Add;

public sealed class AddSparePartHandler : IRequestHandler<AddSparePartCommand, AddSparePartResult?>
{
    private readonly TripDbContext _context;

    public AddSparePartHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<AddSparePartResult?> Handle(AddSparePartCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return null;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        var sparePart = new TripSparePart
        {
            TripId    = command.TripId,
            PartName  = command.PartName,
            Quantity  = command.Quantity,
            UnitCost  = command.UnitCost,
            TotalCost = command.Quantity * command.UnitCost, // auto-computed
            CreatedAt = DateTime.UtcNow
        };

        _context.TripSpareParts.Add(sparePart);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddSparePartResult(
            sparePart.Id,
            sparePart.TripId,
            sparePart.PartName,
            sparePart.Quantity,
            sparePart.UnitCost,
            sparePart.TotalCost,
            sparePart.CreatedAt
        );
    }
}
