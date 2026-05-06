using MediatR;

namespace CustomerService.Api.Features.Customers.Delete;

public sealed record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
