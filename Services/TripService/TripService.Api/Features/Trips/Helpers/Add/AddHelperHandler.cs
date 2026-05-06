using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.Helpers.Add;

public sealed class AddHelperHandler : IRequestHandler<AddHelperCommand, AddHelperResult?>
{
    private readonly TripDbContext _context;

    public AddHelperHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<AddHelperResult?> Handle(AddHelperCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return null;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        var helper = new TripHelper
        {
            TripId     = command.TripId,
            HelperName = command.HelperName,
            Pay        = command.Pay,
            CreatedAt  = DateTime.UtcNow
        };

        _context.TripHelpers.Add(helper);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddHelperResult(
            helper.Id,
            helper.TripId,
            helper.HelperName,
            helper.Pay,
            helper.CreatedAt
        );
    }
}
