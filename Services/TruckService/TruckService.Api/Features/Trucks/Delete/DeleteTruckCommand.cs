using MediatR;

namespace TruckService.Api.Features.Trucks.Delete;

public sealed record DeleteTruckCommand(Guid Id) : IRequest<DeleteTruckResult>;

public sealed record DeleteTruckResult(Guid Id, string Message);
