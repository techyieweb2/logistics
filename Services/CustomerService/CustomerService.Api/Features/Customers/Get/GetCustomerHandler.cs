using CustomerService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Api.Features.Customers.Get;

public sealed class GetCustomerHandler : IRequestHandler<GetCustomerQuery, GetCustomerResult?>
{
    private readonly CustomerDbContext _context;

    public GetCustomerHandler(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<GetCustomerResult?> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (customer is null)
            return null;

        return new GetCustomerResult(
            customer.Id,
            customer.Code,
            customer.Name,
            customer.Address,
            customer.City,
            customer.Province,
            customer.ContactPerson,
            customer.ContactNumber,
            customer.Email,
            customer.IsActive,
            customer.CreatedAt,
            customer.UpdatedAt
        );
    }
}
