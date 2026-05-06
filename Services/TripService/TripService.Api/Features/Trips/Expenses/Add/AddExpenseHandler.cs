using MediatR;
using Microsoft.EntityFrameworkCore;
using TripService.Domain.Entities;
using TripService.Domain.Enums;
using TripService.Infrastructure.Persistence;

namespace TripService.Api.Features.Trips.Expenses.Add;

public sealed class AddExpenseHandler : IRequestHandler<AddExpenseCommand, AddExpenseResult?>
{
    private readonly TripDbContext _context;

    public AddExpenseHandler(TripDbContext context)
    {
        _context = context;
    }

    public async Task<AddExpenseResult?> Handle(AddExpenseCommand command, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.Id == command.TripId, cancellationToken);

        if (trip is null)
            return null;

        if (trip.Status == TripStatus.Closed)
            throw new InvalidOperationException("Cannot modify a closed trip.");

        var expense = new TripExpense
        {
            TripId      = command.TripId,
            ExpenseType = Enum.Parse<ExpenseType>(command.ExpenseType),
            Description = command.Description,
            Amount      = command.Amount,
            CreatedAt   = DateTime.UtcNow
        };

        _context.TripExpenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddExpenseResult(
            expense.Id,
            expense.TripId,
            expense.ExpenseType.ToString(),
            expense.Description,
            expense.Amount,
            expense.CreatedAt
        );
    }
}
