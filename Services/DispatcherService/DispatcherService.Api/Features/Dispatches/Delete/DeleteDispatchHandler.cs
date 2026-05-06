using DispatcherService.Domain.Enums;
using DispatcherService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Api.Features.Dispatches.Delete;

public sealed class DeleteDispatchHandler : IRequestHandler<DeleteDispatchCommand, bool>
{
    private readonly DispatcherDbContext _context;

    public DeleteDispatchHandler(DispatcherDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteDispatchCommand command, CancellationToken cancellationToken)
    {
        var dispatch = await _context.Dispatches
            .FirstOrDefaultAsync(d => d.Id == command.Id, cancellationToken);

        if (dispatch is null)
            return false;

        // Only allow deletion if dispatch is Pending or Cancelled
        if (dispatch.Status is not (DispatchStatus.Pending or DispatchStatus.Cancelled))
            throw new InvalidOperationException("Only Pending or Cancelled dispatches can be deleted.");

        _context.Dispatches.Remove(dispatch);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
