using FluentValidation;

namespace TripService.Api.Features.Trips.Create;

public sealed class CreateTripValidator : AbstractValidator<CreateTripCommand>
{
    private static readonly string[] ValidExpenseTypes = ["TollFee", "Other"];
    private static readonly string[] ValidFuelTypes = ["Diesel", "Gas"];

    public CreateTripValidator()
    {
        RuleFor(x => x.DispatchId)
            .NotEmpty().WithMessage("Dispatch ID is required.");

        // Truck
        RuleFor(x => x.TruckId)
            .NotEmpty().WithMessage("Truck ID is required.");

        RuleFor(x => x.TruckPlateNumber)
            .NotEmpty().WithMessage("Truck plate number is required.")
            .MaximumLength(50).WithMessage("Truck plate number must not exceed 50 characters.");

        // Driver
        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("Driver ID is required.");

        RuleFor(x => x.DriverName)
            .NotEmpty().WithMessage("Driver name is required.")
            .MaximumLength(200).WithMessage("Driver name must not exceed 200 characters.");

        // Customer
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters.");

        // Route
        RuleFor(x => x.RouteName)
            .NotEmpty().WithMessage("Route name is required.")
            .MaximumLength(150).WithMessage("Route name must not exceed 150 characters.");

        RuleFor(x => x.Origin)
            .NotEmpty().WithMessage("Origin is required.")
            .MaximumLength(150).WithMessage("Origin must not exceed 150 characters.");

        RuleFor(x => x.Destination)
            .NotEmpty().WithMessage("Destination is required.")
            .MaximumLength(150).WithMessage("Destination must not exceed 150 characters.");

        // Financials
        RuleFor(x => x.FreightCost)
            .GreaterThan(0).WithMessage("Freight cost must be greater than 0.");

        RuleFor(x => x.DriverPay)
            .GreaterThanOrEqualTo(0).WithMessage("Driver pay must be 0 or greater.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
            .When(x => x.Notes is not null);

        // Expenses
        RuleForEach(x => x.Expenses).ChildRules(e =>
        {
            e.RuleFor(x => x.ExpenseType)
                .Must(t => ValidExpenseTypes.Contains(t))
                .WithMessage("Expense type must be TollFee or Other.");
            e.RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Expense amount must be greater than 0.");
        }).When(x => x.Expenses is not null);

        // Helpers
        RuleForEach(x => x.Helpers).ChildRules(h =>
        {
            h.RuleFor(x => x.HelperName)
                .NotEmpty().WithMessage("Helper name is required.")
                .MaximumLength(200).WithMessage("Helper name must not exceed 200 characters.");
            h.RuleFor(x => x.Pay)
                .GreaterThan(0).WithMessage("Helper pay must be greater than 0.");
        }).When(x => x.Helpers is not null);

        // Fuel
        RuleForEach(x => x.FuelExpenses).ChildRules(f =>
        {
            f.RuleFor(x => x.FuelType)
                .Must(t => ValidFuelTypes.Contains(t))
                .WithMessage("Fuel type must be Diesel or Gas.");
            f.RuleFor(x => x.Liters)
                .GreaterThan(0).WithMessage("Liters must be greater than 0.");
            f.RuleFor(x => x.PricePerLiter)
                .GreaterThan(0).WithMessage("Price per liter must be greater than 0.");
        }).When(x => x.FuelExpenses is not null);

        // Spare Parts
        RuleForEach(x => x.SpareParts).ChildRules(s =>
        {
            s.RuleFor(x => x.PartName)
                .NotEmpty().WithMessage("Part name is required.")
                .MaximumLength(200).WithMessage("Part name must not exceed 200 characters.");
            s.RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            s.RuleFor(x => x.UnitCost)
                .GreaterThan(0).WithMessage("Unit cost must be greater than 0.");
        }).When(x => x.SpareParts is not null);
    }
}
