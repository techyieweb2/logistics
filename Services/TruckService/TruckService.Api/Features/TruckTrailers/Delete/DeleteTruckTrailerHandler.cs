using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckTrailers.Delete;

public sealed class DeleteTruckTrailerHandler : IRequestHandler<DeleteTruckTrailerCommand, DeleteTruckTrailerResult>
{
    private readonly TruckDbContext _context;

    public DeleteTruckTrailerHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteTruckTrailerResult> Handle(DeleteTruckTrailerCommand command, CancellationToken cancellationToken)
    {
        var trailer = await _context.TruckTrailers
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.TruckId == command.TruckId, cancellationToken);

        if (trailer is null)
            throw new KeyNotFoundException($"Trailer with ID '{command.Id}' was not found for Truck '{command.TruckId}'.");

        _context.TruckTrailers.Remove(trailer);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTruckTrailerResult(trailer.Id, $"Trailer '{trailer.TrailerNumber}' has been deleted successfully.");
    }
}
