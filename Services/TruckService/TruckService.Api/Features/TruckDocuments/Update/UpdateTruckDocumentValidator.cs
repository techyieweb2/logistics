using FluentValidation;

namespace TruckService.Api.Features.TruckDocuments.Update;

public sealed class UpdateTruckDocumentValidator : AbstractValidator<UpdateTruckDocumentCommand>
{
    private static readonly string[] AllowedDocumentTypes = ["OR", "CR", "Insurance", "Inspection", "PMS"];

    public UpdateTruckDocumentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Document ID is required.");

        RuleFor(x => x.TruckId)
            .NotEmpty().WithMessage("Truck ID is required.");

        RuleFor(x => x.DocumentType)
            .MaximumLength(50).WithMessage("Document type must not exceed 50 characters.")
            .Must(x => AllowedDocumentTypes.Contains(x))
            .WithMessage($"Document type must be one of: {string.Join(", ", AllowedDocumentTypes)}.")
            .When(x => x.DocumentType is not null);

        RuleFor(x => x.FilePath)
            .MaximumLength(255).WithMessage("File path must not exceed 255 characters.")
            .When(x => x.FilePath is not null);

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiry date must be a future date.")
            .When(x => x.ExpiryDate is not null);
    }
}
