using FluentValidation;

namespace TripService.Api.Features.Trips.UpdateDriverPay;

public sealed class UpdateDriverPayValidator : AbstractValidator<UpdateDriverPayCommand>
{
    public UpdateDriverPayValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty().WithMessage("Trip ID is required.");

        RuleFor(x => x.DriverPay)
            .GreaterThanOrEqualTo(0).WithMessage("Driver pay must be 0 or greater.");
    }
}
