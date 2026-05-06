using FluentValidation;

namespace TripService.Api.Features.Trips.Expenses.Add;

public sealed class AddExpenseValidator : AbstractValidator<AddExpenseCommand>
{
    private static readonly string[] ValidExpenseTypes = ["TollFee", "Other"];

    public AddExpenseValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty().WithMessage("Trip ID is required.");

        RuleFor(x => x.ExpenseType)
            .NotEmpty().WithMessage("Expense type is required.")
            .Must(t => ValidExpenseTypes.Contains(t))
            .WithMessage("Expense type must be TollFee or Other.");

        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("Description must not exceed 255 characters.")
            .When(x => x.Description is not null);

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");
    }
}
