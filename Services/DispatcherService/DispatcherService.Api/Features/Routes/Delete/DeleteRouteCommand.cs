using MediatR;

namespace DispatcherService.Api.Features.Routes.Delete;

public sealed record DeleteRouteCommand(Guid Id) : IRequest<bool>;
