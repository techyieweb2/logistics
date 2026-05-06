using MediatR;

namespace TruckService.Api.Features.TruckDocuments.Delete;

public sealed record DeleteTruckDocumentCommand(Guid TruckId, Guid Id) : IRequest<DeleteTruckDocumentResult>;

public sealed record DeleteTruckDocumentResult(Guid Id, string Message);
