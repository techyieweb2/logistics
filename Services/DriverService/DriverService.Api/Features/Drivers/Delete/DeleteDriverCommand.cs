using MediatR;

namespace DriverService.Api.Features.Drivers.Delete;

public sealed record DeleteDriverCommand(Guid Id) : IRequest<bool>;