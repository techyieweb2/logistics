using DriverService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Api.Features.Drivers.Delete;

public sealed class DeleteDriverHandler : IRequestHandler<DeleteDriverCommand, bool>
{
    private readonly DriverDbContext _context;

    public DeleteDriverHandler(DriverDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteDriverCommand command, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.Id == command.Id, cancellationToken);

        if (driver is null)
            return false;

        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}