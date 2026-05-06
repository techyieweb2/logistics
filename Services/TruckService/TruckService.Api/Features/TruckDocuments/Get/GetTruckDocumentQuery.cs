using MediatR;

namespace TruckService.Api.Features.TruckDocuments.Get;

public sealed record GetTruckDocumentQuery(Guid TruckId, Guid Id) : IRequest<GetTruckDocumentResult>;

public sealed record GetTruckDocumentResult(
    Guid Id,
    Guid TruckId,
    string? DocumentType,
    string? FilePath,
    DateTime? ExpiryDate,
    DateTime CreatedAt
);
