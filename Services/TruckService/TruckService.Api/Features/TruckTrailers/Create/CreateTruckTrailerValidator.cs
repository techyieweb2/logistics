using FluentValidation;

namespace TruckService.Api.Features.TruckTrailers.Create;

public sealed class CreateTruckTrailerValidator : AbstractValidator<CreateTruckTrailerCommand>
{
    private static readonly string[] AllowedTrailerTypes = ["Flatbed", "Curtainsider", "Refrigerated", "Container"];

    public CreateTruckTrailerValidator()
    {
        RuleFor(x => x.TruckId)
            .NotEmpty().WithMessage("Truck ID is required.");

        RuleFor(x => x.TrailerNumber)
            .NotEmpty().WithMessage("Trailer number is required.")
            .MaximumLength(50).WithMessage("Trailer number must not exceed 50 characters.");

        RuleFor(x => x.TrailerType)
            .MaximumLength(50).WithMessage("Trailer type must not exceed 50 characters.")
            .Must(x => AllowedTrailerTypes.Contains(x))
            .WithMessage($"Trailer type must be one of: {string.Join(", ", AllowedTrailerTypes)}.")
            .When(x => x.TrailerType is not null);

        RuleFor(x => x.Capacity)
            .MaximumLength(50).WithMessage("Capacity must not exceed 50 characters.")
            .When(x => x.Capacity is not null);
    }
}
