using MediatR;

namespace TruckService.Api.Features.TruckDocuments.Update;

public sealed record UpdateTruckDocumentCommand(
    Guid Id,
    Guid TruckId,
    string? DocumentType,
    string? FilePath,
    DateTime? ExpiryDate
) : IRequest<UpdateTruckDocumentResult>;

public sealed record UpdateTruckDocumentResult(Guid Id, Guid TruckId, string? DocumentType);
