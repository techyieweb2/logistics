using FluentValidation;

namespace TruckService.Api.Features.Trucks.Create;

public sealed class CreateTruckValidator : AbstractValidator<CreateTruckCommand>
{
    public CreateTruckValidator()
    {
        RuleFor(x => x.PlateNumber)
            .NotEmpty().WithMessage("Plate number is required.")
            .MaximumLength(20).WithMessage("Plate number must not exceed 20 characters.");

        RuleFor(x => x.ChassisNumber)
            .MaximumLength(100).WithMessage("Chassis number must not exceed 100 characters.")
            .When(x => x.ChassisNumber is not null);

        RuleFor(x => x.EngineNumber)
            .MaximumLength(100).WithMessage("Engine number must not exceed 100 characters.")
            .When(x => x.EngineNumber is not null);

        RuleFor(x => x.Brand)
            .MaximumLength(100).WithMessage("Brand must not exceed 100 characters.")
            .When(x => x.Brand is not null);

        RuleFor(x => x.Model)
            .MaximumLength(100).WithMessage("Model must not exceed 100 characters.")
            .When(x => x.Model is not null);

        RuleFor(x => x.YearModel)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage($"Year model must be between 1900 and {DateTime.UtcNow.Year}.")
            .When(x => x.YearModel is not null);

        RuleFor(x => x.Color)
            .MaximumLength(50).WithMessage("Color must not exceed 50 characters.")
            .When(x => x.Color is not null);

        RuleFor(x => x.TruckType)
            .MaximumLength(50).WithMessage("Truck type must not exceed 50 characters.")
            .When(x => x.TruckType is not null);

        RuleFor(x => x.GrossVehicleWeight)
            .MaximumLength(50).WithMessage("Gross vehicle weight must not exceed 50 characters.")
            .When(x => x.GrossVehicleWeight is not null);

        RuleFor(x => x.Capacity)
            .MaximumLength(50).WithMessage("Capacity must not exceed 50 characters.")
            .When(x => x.Capacity is not null);

        RuleFor(x => x.FuelType)
            .MaximumLength(30).WithMessage("Fuel type must not exceed 30 characters.")
            .Must(x => x == "Diesel" || x == "Gasoline")
            .WithMessage("Fuel type must be either 'Diesel' or 'Gasoline'.")
            .When(x => x.FuelType is not null);

        RuleFor(x => x.AcquisitionDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Acquisition date must not be a future date.")
            .When(x => x.AcquisitionDate is not null);
    }
}
