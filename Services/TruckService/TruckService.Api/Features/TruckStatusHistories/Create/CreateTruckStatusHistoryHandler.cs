using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Domain.Entities;
using TruckService.Domain.Enums;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckStatusHistories.Create;

public sealed class CreateTruckStatusHistoryHandler : IRequestHandler<CreateTruckStatusHistoryCommand, CreateTruckStatusHistoryResult>
{
    private readonly TruckDbContext _context;

    public CreateTruckStatusHistoryHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTruckStatusHistoryResult> Handle(CreateTruckStatusHistoryCommand command, CancellationToken cancellationToken)
    {
        var truck = await _context.Trucks
            .FirstOrDefaultAsync(x => x.Id == command.TruckId, cancellationToken);

        if (truck is null)
            throw new KeyNotFoundException($"Truck with ID '{command.TruckId}' was not found.");

        var newStatus = (TruckStatus)command.Status;

        // Update the truck's current status
        truck.Status    = newStatus;
        truck.UpdatedAt = DateTime.UtcNow;

        // Append to history
        var history = new TruckStatusHistory
        {
            TruckId   = command.TruckId,
            Status    = newStatus,
            ChangedAt = DateTime.UtcNow,
            Notes     = command.Notes
        };

        _context.TruckStatusHistories.Add(history);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTruckStatusHistoryResult(history.Id, history.TruckId, newStatus.ToString(), history.ChangedAt);
    }
}
