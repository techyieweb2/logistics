using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.Helpers.Delete;

public sealed class DeleteHelperHandler : IRequestHandler<DeleteHelperCommand, bool>
{
    private readonly TripDbContext _context;

    public DeleteHelperHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteHelperCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return false;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        var helper = await _context.TripHelpers
            .FirstOrDefaultAsync(h => h.Id == command.HelperId && h.TripId == command.TripId, cancellationToken);

        if (helper is null)
            return false;

        _context.TripHelpers.Remove(helper);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
