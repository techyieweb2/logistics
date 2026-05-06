using MediatR;

namespace CustomerService.Api.Features.Customers.Update;

public sealed record UpdateCustomerCommand(
    Guid Id,
    string Name,
    string? Address,
    string? City,
    string? Province,
    string? ContactPerson,
    string? ContactNumber,
    string? Email,
    bool IsActive
) : IRequest<UpdateCustomerResult?>;

public sealed record UpdateCustomerResult(
    Guid Id,
    string Code,
    string Name,
    bool IsActive,
    DateTime UpdatedAt
);
