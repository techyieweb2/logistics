using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckDocuments.GetAll;

public sealed class GetAllTruckDocumentsHandler : IRequestHandler<GetAllTruckDocumentsQuery, IEnumerable<GetAllTruckDocumentsResult>>
{
    private readonly TruckDbContext _context;

    public GetAllTruckDocumentsHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllTruckDocumentsResult>> Handle(GetAllTruckDocumentsQuery query, CancellationToken cancellationToken)
    {
        return await _context.TruckDocuments
            .AsNoTracking()
            .Where(x => x.TruckId == query.TruckId)
            .OrderBy(x => x.DocumentType)
            .Select(x => new GetAllTruckDocumentsResult(
                x.Id,
                x.TruckId,
                x.DocumentType,
                x.FilePath,
                x.ExpiryDate,
                x.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}
