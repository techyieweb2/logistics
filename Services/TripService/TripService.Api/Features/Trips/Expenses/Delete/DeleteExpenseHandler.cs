using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.Expenses.Delete;

public sealed class DeleteExpenseHandler : IRequestHandler<DeleteExpenseCommand, bool>
{
    private readonly TripDbContext _context;

    public DeleteExpenseHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return false;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        var expense = await _context.TripExpenses
            .FirstOrDefaultAsync(e => e.Id == command.ExpenseId && e.TripId == command.TripId, cancellationToken);

        if (expense is null)
            return false;

        _context.TripExpenses.Remove(expense);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
