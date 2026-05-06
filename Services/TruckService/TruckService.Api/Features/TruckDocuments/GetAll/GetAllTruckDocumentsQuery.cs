using MediatR;

namespace TruckService.Api.Features.TruckDocuments.GetAll;

public sealed record GetAllTruckDocumentsQuery(Guid TruckId) : IRequest<IEnumerable<GetAllTruckDocumentsResult>>;

public sealed record GetAllTruckDocumentsResult(
    Guid Id,
    Guid TruckId,
    string? DocumentType,
    string? FilePath,
    DateTime? ExpiryDate,
    DateTime CreatedAt
);
