using FluentValidation;

namespace TripService.Api.Features.Trips.FuelExpenses.Add;

public sealed class AddFuelValidator : AbstractValidator<AddFuelCommand>
{
    private static readonly string[] ValidFuelTypes = ["Diesel", "Gas"];

    public AddFuelValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty().WithMessage("Trip ID is required.");

        RuleFor(x => x.FuelType)
            .NotEmpty().WithMessage("Fuel type is required.")
            .Must(t => ValidFuelTypes.Contains(t))
            .WithMessage("Fuel type must be Diesel or Gas.");

        RuleFor(x => x.Liters)
            .GreaterThan(0).WithMessage("Liters must be greater than 0.");

        RuleFor(x => x.PricePerLiter)
            .GreaterThan(0).WithMessage("Price per liter must be greater than 0.");

        RuleFor(x => x.Station)
            .MaximumLength(150).WithMessage("Station must not exceed 150 characters.")
            .When(x => x.Station is not null);
    }
}
