using MediatR;

namespace TripService.Api.Features.Trips.Expenses.Add;

public sealed record AddExpenseCommand(
    Guid TripId,
    string ExpenseType,
    string? Description,
    decimal Amount
) : IRequest<AddExpenseResult?>;

public sealed record AddExpenseResult(
    Guid Id,
    Guid TripId,
    string ExpenseType,
    string? Description,
    decimal Amount,
    DateTime CreatedAt
);
