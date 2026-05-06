using MediatR;

namespace TripService.Api.Features.Trips.Expenses.Delete;

public sealed record DeleteExpenseCommand(Guid TripId, Guid ExpenseId) : IRequest<bool>;
