using FluentValidation;

namespace DriverService.Api.Features.Drivers.Update;

public sealed class UpdateDriverValidator : AbstractValidator<UpdateDriverCommand>
{
    public UpdateDriverValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Driver ID is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.MiddleName)
            .MaximumLength(100).WithMessage("Middle name must not exceed 100 characters.")
            .When(x => x.MiddleName is not null);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

        RuleFor(x => x.Gender)
            .MaximumLength(10).WithMessage("Gender must not exceed 10 characters.")
            .When(x => x.Gender is not null);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth must be in the past.")
            .When(x => x.DateOfBirth is not null);

        RuleFor(x => x.Height)
            .MaximumLength(20).WithMessage("Height must not exceed 20 characters.")
            .When(x => x.Height is not null);

        RuleFor(x => x.Weight)
            .MaximumLength(20).WithMessage("Weight must not exceed 20 characters.")
            .When(x => x.Weight is not null);

        RuleFor(x => x.Religion)
            .MaximumLength(50).WithMessage("Religion must not exceed 50 characters.")
            .When(x => x.Religion is not null);

        RuleFor(x => x.CivilStatus)
            .MaximumLength(30).WithMessage("Civil status must not exceed 30 characters.")
            .When(x => x.CivilStatus is not null);

        RuleFor(x => x.CivilStatusPlace)
            .MaximumLength(150).WithMessage("Civil status place must not exceed 150 characters.")
            .When(x => x.CivilStatusPlace is not null);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.")
            .When(x => x.PhoneNumber is not null);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is not valid.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.")
            .When(x => x.Email is not null);

        RuleFor(x => x.LicenseNumber)
            .MaximumLength(50).WithMessage("License number must not exceed 50 characters.")
            .When(x => x.LicenseNumber is not null);

        RuleFor(x => x.SssNumber)
            .MaximumLength(30).WithMessage("SSS number must not exceed 30 characters.")
            .When(x => x.SssNumber is not null);

        RuleFor(x => x.PhilHealthNumber)
            .MaximumLength(30).WithMessage("PhilHealth number must not exceed 30 characters.")
            .When(x => x.PhilHealthNumber is not null);

        RuleFor(x => x.PagIbigNumber)
            .MaximumLength(30).WithMessage("PAG-IBIG number must not exceed 30 characters.")
            .When(x => x.PagIbigNumber is not null);

        RuleFor(x => x.TinNumber)
            .MaximumLength(30).WithMessage("TIN number must not exceed 30 characters.")
            .When(x => x.TinNumber is not null);

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(s => new[] { "Active", "Inactive", "Suspended" }.Contains(s))
            .WithMessage("Status must be Active, Inactive, or Suspended.");
    }
}