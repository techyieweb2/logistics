using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckDocuments.Get;

public sealed class GetTruckDocumentHandler : IRequestHandler<GetTruckDocumentQuery, GetTruckDocumentResult>
{
    private readonly TruckDbContext _context;

    public GetTruckDocumentHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<GetTruckDocumentResult> Handle(GetTruckDocumentQuery query, CancellationToken cancellationToken)
    {
        var document = await _context.TruckDocuments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id && x.TruckId == query.TruckId, cancellationToken);

        if (document is null)
            throw new KeyNotFoundException($"Document with ID '{query.Id}' was not found for Truck '{query.TruckId}'.");

        return new GetTruckDocumentResult(
            document.Id,
            document.TruckId,
            document.DocumentType,
            document.FilePath,
            document.ExpiryDate,
            document.CreatedAt
        );
    }
}
