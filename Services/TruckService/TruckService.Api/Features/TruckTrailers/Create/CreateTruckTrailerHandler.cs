using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Domain.Entities;
using TruckService.Domain.Enums;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckTrailers.Create;

public sealed class CreateTruckTrailerHandler : IRequestHandler<CreateTruckTrailerCommand, CreateTruckTrailerResult>
{
    private readonly TruckDbContext _context;

    public CreateTruckTrailerHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTruckTrailerResult> Handle(CreateTruckTrailerCommand command, CancellationToken cancellationToken)
    {
        var truckExists = await _context.Trucks
            .AnyAsync(x => x.Id == command.TruckId, cancellationToken);

        if (!truckExists)
            throw new KeyNotFoundException($"Truck with ID '{command.TruckId}' was not found.");

        var duplicateTrailerNumber = await _context.TruckTrailers
            .AnyAsync(x => x.TrailerNumber == command.TrailerNumber, cancellationToken);

        if (duplicateTrailerNumber)
            throw new InvalidOperationException($"Trailer number '{command.TrailerNumber}' is already in use.");

        var trailer = new TruckTrailer
        {
            TruckId       = command.TruckId,
            TrailerNumber = command.TrailerNumber,
            TrailerType   = command.TrailerType,
            Capacity      = command.Capacity,
            Status        = TrailerStatus.Available,
            CreatedAt     = DateTime.UtcNow
        };

        _context.TruckTrailers.Add(trailer);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTruckTrailerResult(trailer.Id, trailer.TruckId, trailer.TrailerNumber, trailer.TrailerType);
    }
}
