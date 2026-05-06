using MediatR;

namespace CustomerService.Api.Features.Customers.Get;

public sealed record GetCustomerQuery(Guid Id) : IRequest<GetCustomerResult?>;

public sealed record GetCustomerResult(
    Guid Id,
    string Code,
    string Name,
    string? Address,
    string? City,
    string? Province,
    string? ContactPerson,
    string? ContactNumber,
    string? Email,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
