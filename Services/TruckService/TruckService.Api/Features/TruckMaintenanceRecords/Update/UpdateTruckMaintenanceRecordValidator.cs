using FluentValidation;

namespace TruckService.Api.Features.TruckMaintenanceRecords.Update;

public sealed class UpdateTruckMaintenanceRecordValidator : AbstractValidator<UpdateTruckMaintenanceRecordCommand>
{
    private static readonly string[] AllowedMaintenanceTypes = ["Preventive", "Corrective", "Tire Change", "Oil Change"];

    public UpdateTruckMaintenanceRecordValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Maintenance record ID is required.");

        RuleFor(x => x.TruckId)
            .NotEmpty().WithMessage("Truck ID is required.");

        RuleFor(x => x.MaintenanceType)
            .MaximumLength(100).WithMessage("Maintenance type must not exceed 100 characters.")
            .Must(x => AllowedMaintenanceTypes.Contains(x))
            .WithMessage($"Maintenance type must be one of: {string.Join(", ", AllowedMaintenanceTypes)}.")
            .When(x => x.MaintenanceType is not null);

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(x => x.Description is not null);

        RuleFor(x => x.OdometerReading)
            .MaximumLength(50).WithMessage("Odometer reading must not exceed 50 characters.")
            .When(x => x.OdometerReading is not null);

        RuleFor(x => x.ServiceDate)
            .NotEmpty().WithMessage("Service date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Service date must not be a future date.");

        RuleFor(x => x.ServicedBy)
            .MaximumLength(200).WithMessage("Serviced by must not exceed 200 characters.")
            .When(x => x.ServicedBy is not null);

        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).WithMessage("Cost must not be negative.")
            .When(x => x.Cost is not null);

        RuleFor(x => x.NextServiceDate)
            .GreaterThan(x => x.ServiceDate).WithMessage("Next service date must be after the service date.")
            .When(x => x.NextServiceDate is not null);
    }
}
