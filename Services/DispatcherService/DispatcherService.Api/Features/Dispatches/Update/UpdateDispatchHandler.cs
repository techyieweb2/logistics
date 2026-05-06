using DispatcherService.Domain.Entities;
using DispatcherService.Domain.Enums;
using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Dispatches.Update;

public sealed class UpdateDispatchHandler : IRequestHandler<UpdateDispatchCommand, UpdateDispatchResult?>
{
    private readonly DispatcherDbContext _context;

    public UpdateDispatchHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateDispatchResult?> Handle(UpdateDispatchCommand command, CancellationToken cancellationToken)
    {
        var dispatch = await _context.Dispatches
            .Include(d => d.StatusHistories)
            .FirstOrDefaultAsync(d => d.Id == command.Id, cancellationToken);

        if (dispatch is null)
            return null;

        var newStatus = Enum.Parse<DispatchStatus>(command.Status);

        // Track status change in history if status changed
        if (dispatch.Status != newStatus)
        {
            dispatch.StatusHistories.Add(new DispatchStatusHistory
            {
                DispatchId = dispatch.Id,
                Status     = newStatus,
                ChangedAt  = DateTime.UtcNow,
                Remarks    = command.StatusRemarks,
                CreatedAt  = DateTime.UtcNow
            });
        }

        dispatch.DriverId       = command.DriverId;
        dispatch.TruckId        = command.TruckId;
        dispatch.RouteId        = command.RouteId;
        dispatch.CustomerId     = command.CustomerId;
        dispatch.Status         = newStatus;
        dispatch.ScheduledDate  = command.ScheduledDate;
        dispatch.DepartureDate  = command.DepartureDate;
        dispatch.ArrivalDate    = command.ArrivalDate;
        dispatch.Notes          = command.Notes;
        dispatch.UpdatedAt      = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateDispatchResult(
            dispatch.Id,
            dispatch.ReferenceNumber,
            dispatch.Status.ToString(),
            dispatch.UpdatedAt!.Value
        );
    }
}
