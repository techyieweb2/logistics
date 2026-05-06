using CustomerService.Domain.Entities;
using CustomerService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Api.Features.Customers.Create;

public sealed class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
{
    private readonly CustomerDbContext _context;

    public CreateCustomerHandler(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<CreateCustomerResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var code = await GenerateCodeAsync(cancellationToken);

        var customer = new Customer
        {
            Code          = code,
            Name          = command.Name,
            Address       = command.Address,
            City          = command.City,
            Province      = command.Province,
            ContactPerson = command.ContactPerson,
            ContactNumber = command.ContactNumber,
            Email         = command.Email,
            IsActive      = true,
            CreatedAt     = DateTime.UtcNow
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCustomerResult(
            customer.Id,
            customer.Code,
            customer.Name,
            customer.IsActive
        );
    }

    private async Task<string> GenerateCodeAsync(CancellationToken cancellationToken)
    {
        var last = await _context.Customers
            .OrderByDescending(c => c.Code)
            .FirstOrDefaultAsync(cancellationToken);

        var sequence = 1;
        if (last is not null)
        {
            var lastNumber = last.Code.Split('-').Last();
            if (int.TryParse(lastNumber, out var parsed))
                sequence = parsed + 1;
        }

        return $"CUST-{sequence:D4}";
    }
}
