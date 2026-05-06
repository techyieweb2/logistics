using CustomerService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Api.Features.Customers.Update;

public sealed class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResult?>
{
    private readonly CustomerDbContext _context;

    public UpdateCustomerHandler(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateCustomerResult?> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (customer is null)
            return null;

        customer.Name          = command.Name;
        customer.Address       = command.Address;
        customer.City          = command.City;
        customer.Province      = command.Province;
        customer.ContactPerson = command.ContactPerson;
        customer.ContactNumber = command.ContactNumber;
        customer.Email         = command.Email;
        customer.IsActive      = command.IsActive;
        customer.UpdatedAt     = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateCustomerResult(
            customer.Id,
            customer.Code,
            customer.Name,
            customer.IsActive,
            customer.UpdatedAt!.Value
        );
    }
}
