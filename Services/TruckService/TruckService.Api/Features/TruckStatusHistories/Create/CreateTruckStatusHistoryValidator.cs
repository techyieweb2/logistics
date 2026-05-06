using FluentValidation;
using TruckService.Domain.Enums;

namespace TruckService.Api.Features.TruckStatusHistories.Create;

public sealed class CreateTruckStatusHistoryValidator : AbstractValidator<CreateTruckStatusHistoryCommand>
{
    public CreateTruckStatusHistoryValidator()
    {
        RuleFor(x => x.TruckId)
            .NotEmpty().WithMessage("Truck ID is required.");

        RuleFor(x => x.Status)
            .Must(x => Enum.IsDefined(typeof(TruckStatus), x))
            .WithMessage("Status must be a valid value: 0=Inactive, 1=Available, 2=In-Use, 3=Under Maintenance, 4=Retired.");

        RuleFor(x => x.Notes)
            .MaximumLength(255).WithMessage("Notes must not exceed 255 characters.")
            .When(x => x.Notes is not null);
    }
}
