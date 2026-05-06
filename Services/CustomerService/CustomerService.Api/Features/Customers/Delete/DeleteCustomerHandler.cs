using CustomerService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Api.Features.Customers.Delete;

public sealed class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly CustomerDbContext _context;

    public DeleteCustomerHandler(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (customer is null)
            return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
