using FluentValidation;

namespace CustomerService.Api.Features.Customers.Create;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters.");

        RuleFor(x => x.Address)
            .MaximumLength(255).WithMessage("Address must not exceed 255 characters.")
            .When(x => x.Address is not null);

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.")
            .When(x => x.City is not null);

        RuleFor(x => x.Province)
            .MaximumLength(100).WithMessage("Province must not exceed 100 characters.")
            .When(x => x.Province is not null);

        RuleFor(x => x.ContactPerson)
            .MaximumLength(200).WithMessage("Contact person must not exceed 200 characters.")
            .When(x => x.ContactPerson is not null);

        RuleFor(x => x.ContactNumber)
            .MaximumLength(20).WithMessage("Contact number must not exceed 20 characters.")
            .When(x => x.ContactNumber is not null);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is not valid.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.")
            .When(x => x.Email is not null);
    }
}
