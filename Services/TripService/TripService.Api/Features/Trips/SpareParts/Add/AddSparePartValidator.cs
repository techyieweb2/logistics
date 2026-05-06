using FluentValidation;

namespace TripService.Api.Features.Trips.SpareParts.Add;

public sealed class AddSparePartValidator : AbstractValidator<AddSparePartCommand>
{
    public AddSparePartValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty().WithMessage("Trip ID is required.");

        RuleFor(x => x.PartName)
            .NotEmpty().WithMessage("Part name is required.")
            .MaximumLength(200).WithMessage("Part name must not exceed 200 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.UnitCost)
            .GreaterThan(0).WithMessage("Unit cost must be greater than 0.");
    }
}
