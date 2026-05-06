using FluentValidation;

namespace DispatcherService.Api.Features.Dispatches.Update;

public sealed class UpdateDispatchValidator : AbstractValidator<UpdateDispatchCommand>
{
    private static readonly string[] ValidStatuses = ["Pending", "InTransit", "Completed", "Cancelled"];

    public UpdateDispatchValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Dispatch ID is required.");

        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("Driver ID is required.");

        RuleFor(x => x.TruckId)
            .NotEmpty().WithMessage("Truck ID is required.");

        RuleFor(x => x.RouteId)
            .NotEmpty().WithMessage("Route ID is required.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(s => ValidStatuses.Contains(s))
            .WithMessage("Status must be Pending, InTransit, Completed, or Cancelled.");

        RuleFor(x => x.ScheduledDate)
            .NotEmpty().WithMessage("Scheduled date is required.");

        RuleFor(x => x.DepartureDate)
            .LessThanOrEqualTo(x => x.ArrivalDate)
            .WithMessage("Departure date must be before or equal to arrival date.")
            .When(x => x.DepartureDate is not null && x.ArrivalDate is not null);

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
            .When(x => x.Notes is not null);

        RuleFor(x => x.StatusRemarks)
            .MaximumLength(255).WithMessage("Status remarks must not exceed 255 characters.")
            .When(x => x.StatusRemarks is not null);
    }
}
