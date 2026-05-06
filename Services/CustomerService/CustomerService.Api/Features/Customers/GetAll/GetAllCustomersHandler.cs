using CustomerService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Api.Features.Customers.GetAll;

public sealed class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomersResult>>
{
    private readonly CustomerDbContext _context;

    public GetAllCustomersHandler(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllCustomersResult>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new GetAllCustomersResult(
                c.Id,
                c.Code,
                c.Name,
                c.City,
                c.Province,
                c.ContactNumber,
                c.IsActive,
                c.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}
