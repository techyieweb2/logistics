using DispatcherService.Domain.Entities;
using DispatcherService.Domain.Enums;
using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Dispatches.Create;

public sealed class CreateDispatchHandler : IRequestHandler<CreateDispatchCommand, CreateDispatchResult>
{
    private readonly DispatcherDbContext _context;

    public CreateDispatchHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<CreateDispatchResult> Handle(CreateDispatchCommand command, CancellationToken cancellationToken)
    {
        // Validate route exists
        var routeExists = await _context.Routes
            .AnyAsync(r => r.Id == command.RouteId && r.IsActive, cancellationToken);

        if (!routeExists)
            throw new InvalidOperationException("Route not found or inactive.");

        var referenceNumber = await GenerateReferenceNumberAsync(cancellationToken);

        var dispatch = new Dispatch
        {
            ReferenceNumber = referenceNumber,
            DriverId        = command.DriverId,
            TruckId         = command.TruckId,
            RouteId         = command.RouteId,
            CustomerId      = command.CustomerId,
            Status          = DispatchStatus.Pending,
            ScheduledDate   = command.ScheduledDate,
            Notes           = command.Notes,
            CreatedAt       = DateTime.UtcNow
        };

        // Add initial status history
        dispatch.StatusHistories.Add(new DispatchStatusHistory
        {
            DispatchId = dispatch.Id,
            Status     = DispatchStatus.Pending,
            ChangedAt  = DateTime.UtcNow,
            Remarks    = "Dispatch created.",
            CreatedAt  = DateTime.UtcNow
        });

        _context.Dispatches.Add(dispatch);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateDispatchResult(
            dispatch.Id,
            dispatch.ReferenceNumber,
            dispatch.DriverId,
            dispatch.TruckId,
            dispatch.RouteId,
            dispatch.CustomerId,
            dispatch.Status.ToString(),
            dispatch.ScheduledDate
        );
    }

    private async Task<string> GenerateReferenceNumberAsync(CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.ToString("yyyyMMdd");
        var prefix = $"DISP-{today}-";

        var lastDispatch = await _context.Dispatches
            .Where(d => d.ReferenceNumber.StartsWith(prefix))
            .OrderByDescending(d => d.ReferenceNumber)
            .FirstOrDefaultAsync(cancellationToken);

        var sequence = 1;
        if (lastDispatch is not null)
        {
            var lastSequence = lastDispatch.ReferenceNumber.Split('-').Last();
            if (int.TryParse(lastSequence, out var parsed))
                sequence = parsed + 1;
        }

        return $"{prefix}{sequence:D3}";
    }
}
