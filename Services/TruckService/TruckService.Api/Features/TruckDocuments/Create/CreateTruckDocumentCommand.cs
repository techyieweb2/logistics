using MediatR;

namespace TruckService.Api.Features.TruckDocuments.Create;

public sealed record CreateTruckDocumentCommand(
    Guid TruckId,
    string? DocumentType,
    string? FilePath,
    DateTime? ExpiryDate
) : IRequest<CreateTruckDocumentResult>;

public sealed record CreateTruckDocumentResult(Guid Id, Guid TruckId, string? DocumentType);
