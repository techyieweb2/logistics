using FluentValidation;

namespace TruckService.Api.Features.TruckTrailers.Update;

public sealed class UpdateTruckTrailerValidator : AbstractValidator<UpdateTruckTrailerCommand>
{
    private static readonly string[] AllowedTrailerTypes = ["Flatbed", "Curtainsider", "Refrigerated", "Container"];

    public UpdateTruckTrailerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Trailer ID is required.");

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
