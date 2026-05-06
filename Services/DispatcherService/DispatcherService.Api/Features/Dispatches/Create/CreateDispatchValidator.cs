using FluentValidation;

namespace DispatcherService.Api.Features.Dispatches.Create;

public sealed class CreateDispatchValidator : AbstractValidator<CreateDispatchCommand>
{
    public CreateDispatchValidator()
    {
        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("Driver ID is required.");

        RuleFor(x => x.TruckId)
            .NotEmpty().WithMessage("Truck ID is required.");

        RuleFor(x => x.RouteId)
            .NotEmpty().WithMessage("Route ID is required.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.ScheduledDate)
            .NotEmpty().WithMessage("Scheduled date is required.")
            .GreaterThan(DateTime.UtcNow).WithMessage("Scheduled date must be in the future.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
            .When(x => x.Notes is not null);
    }
}
