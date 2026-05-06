using MediatR;

namespace DispatcherService.Api.Features.Dispatches.Delete;

public sealed record DeleteDispatchCommand(Guid Id) : IRequest<bool>;
