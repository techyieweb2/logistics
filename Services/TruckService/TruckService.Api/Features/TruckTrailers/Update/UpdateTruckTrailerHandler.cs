using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckTrailers.Update;

public sealed class UpdateTruckTrailerHandler : IRequestHandler<UpdateTruckTrailerCommand, UpdateTruckTrailerResult>
{
    private readonly TruckDbContext _context;

    public UpdateTruckTrailerHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateTruckTrailerResult> Handle(UpdateTruckTrailerCommand command, CancellationToken cancellationToken)
    {
        var trailer = await _context.TruckTrailers
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.TruckId == command.TruckId, cancellationToken);

        if (trailer is null)
            throw new KeyNotFoundException($"Trailer with ID '{command.Id}' was not found for Truck '{command.TruckId}'.");

        var duplicateTrailerNumber = await _context.TruckTrailers
            .AnyAsync(x => x.TrailerNumber == command.TrailerNumber && x.Id != command.Id, cancellationToken);

        if (duplicateTrailerNumber)
            throw new InvalidOperationException($"Trailer number '{command.TrailerNumber}' is already in use.");

        trailer.TrailerNumber = command.TrailerNumber;
        trailer.TrailerType   = command.TrailerType;
        trailer.Capacity      = command.Capacity;
        trailer.UpdatedAt     = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateTruckTrailerResult(trailer.Id, trailer.TruckId, trailer.TrailerNumber, trailer.TrailerType);
    }
}
