using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckDocuments.Update;

public sealed class UpdateTruckDocumentHandler : IRequestHandler<UpdateTruckDocumentCommand, UpdateTruckDocumentResult>
{
    private readonly TruckDbContext _context;

    public UpdateTruckDocumentHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateTruckDocumentResult> Handle(UpdateTruckDocumentCommand command, CancellationToken cancellationToken)
    {
        var document = await _context.TruckDocuments
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.TruckId == command.TruckId, cancellationToken);

        if (document is null)
            throw new KeyNotFoundException($"Document with ID '{command.Id}' was not found for Truck '{command.TruckId}'.");

        document.DocumentType = command.DocumentType;
        document.FilePath     = command.FilePath;
        document.ExpiryDate   = command.ExpiryDate;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateTruckDocumentResult(document.Id, document.TruckId, document.DocumentType);
    }
}
