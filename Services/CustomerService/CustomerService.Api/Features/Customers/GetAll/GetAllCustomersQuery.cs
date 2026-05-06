using MediatR;

namespace CustomerService.Api.Features.Customers.GetAll;

public sealed record GetAllCustomersQuery() : IRequest<IEnumerable<GetAllCustomersResult>>;

public sealed record GetAllCustomersResult(
    Guid Id,
    string Code,
    string Name,
    string? City,
    string? Province,
    string? ContactNumber,
    bool IsActive,
    DateTime CreatedAt
);
