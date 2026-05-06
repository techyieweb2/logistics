using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.Trucks.Delete;

public sealed class DeleteTruckHandler : IRequestHandler<DeleteTruckCommand, DeleteTruckResult>
{
    private readonly TruckDbContext _context;

    public DeleteTruckHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteTruckResult> Handle(DeleteTruckCommand command, CancellationToken cancellationToken)
    {
        var truck = await _context.Trucks
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (truck is null)
            throw new KeyNotFoundException($"Truck with ID '{command.Id}' was not found.");

        _context.Trucks.Remove(truck);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTruckResult(truck.Id, $"Truck '{truck.PlateNumber}' has been deleted successfully.");
    }
}
