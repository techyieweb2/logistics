using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckDocuments.Delete;

public sealed class DeleteTruckDocumentHandler : IRequestHandler<DeleteTruckDocumentCommand, DeleteTruckDocumentResult>
{
    private readonly TruckDbContext _context;

    public DeleteTruckDocumentHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteTruckDocumentResult> Handle(DeleteTruckDocumentCommand command, CancellationToken cancellationToken)
    {
        var document = await _context.TruckDocuments
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.TruckId == command.TruckId, cancellationToken);

        if (document is null)
            throw new KeyNotFoundException($"Document with ID '{command.Id}' was not found for Truck '{command.TruckId}'.");

        _context.TruckDocuments.Remove(document);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTruckDocumentResult(document.Id, $"Document '{document.DocumentType}' has been deleted successfully.");
    }
}
