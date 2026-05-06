using MediatR;

namespace TruckService.Api.Features.TruckTrailers.Delete;

public sealed record DeleteTruckTrailerCommand(Guid TruckId, Guid Id) : IRequest<DeleteTruckTrailerResult>;

public sealed record DeleteTruckTrailerResult(Guid Id, string Message);
