using FluentValidation;

namespace DispatcherService.Api.Features.Routes.Update;

public sealed class UpdateRouteValidator : AbstractValidator<UpdateRouteCommand>
{
    public UpdateRouteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Route ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Route name is required.")
            .MaximumLength(150).WithMessage("Route name must not exceed 150 characters.");

        RuleFor(x => x.Origin)
            .NotEmpty().WithMessage("Origin is required.")
            .MaximumLength(150).WithMessage("Origin must not exceed 150 characters.");

        RuleFor(x => x.Destination)
            .NotEmpty().WithMessage("Destination is required.")
            .MaximumLength(150).WithMessage("Destination must not exceed 150 characters.");

        RuleFor(x => x.EstimatedHours)
            .GreaterThan(0).WithMessage("Estimated hours must be greater than 0.")
            .When(x => x.EstimatedHours is not null);

        RuleFor(x => x.DistanceKm)
            .GreaterThan(0).WithMessage("Distance must be greater than 0.")
            .When(x => x.DistanceKm is not null);
    }
}
