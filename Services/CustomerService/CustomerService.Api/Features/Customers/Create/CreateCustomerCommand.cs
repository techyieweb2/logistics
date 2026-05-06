using MediatR;

namespace CustomerService.Api.Features.Customers.Create;

public sealed record CreateCustomerCommand(
    string Name,
    string? Address,
    string? City,
    string? Province,
    string? ContactPerson,
    string? ContactNumber,
    string? Email
) : IRequest<CreateCustomerResult>;

public sealed record CreateCustomerResult(
    Guid Id,
    string Code,
    string Name,
    bool IsActive
);
