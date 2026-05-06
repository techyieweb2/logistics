using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Domain.Entities;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.TruckDocuments.Create;

public sealed class CreateTruckDocumentHandler : IRequestHandler<CreateTruckDocumentCommand, CreateTruckDocumentResult>
{
    private readonly TruckDbContext _context;

    public CreateTruckDocumentHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTruckDocumentResult> Handle(CreateTruckDocumentCommand command, CancellationToken cancellationToken)
    {
        var truckExists = await _context.Trucks
            .AnyAsync(x => x.Id == command.TruckId, cancellationToken);

        if (!truckExists)
            throw new KeyNotFoundException($"Truck with ID '{command.TruckId}' was not found.");

        var document = new TruckDocument
        {
            TruckId      = command.TruckId,
            DocumentType = command.DocumentType,
            FilePath     = command.FilePath,
            ExpiryDate   = command.ExpiryDate,
            CreatedAt    = DateTime.UtcNow
        };

        _context.TruckDocuments.Add(document);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTruckDocumentResult(document.Id, document.TruckId, document.DocumentType);
    }
}
