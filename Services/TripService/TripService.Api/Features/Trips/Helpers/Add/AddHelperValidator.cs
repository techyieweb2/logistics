using FluentValidation;

namespace TripService.Api.Features.Trips.Helpers.Add;

public sealed class AddHelperValidator : AbstractValidator<AddHelperCommand>
{
    public AddHelperValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty().WithMessage("Trip ID is required.");

        RuleFor(x => x.HelperName)
            .NotEmpty().WithMessage("Helper name is required.")
            .MaximumLength(200).WithMessage("Helper name must not exceed 200 characters.");

        RuleFor(x => x.Pay)
            .GreaterThan(0).WithMessage("Pay must be greater than 0.");
    }
}
