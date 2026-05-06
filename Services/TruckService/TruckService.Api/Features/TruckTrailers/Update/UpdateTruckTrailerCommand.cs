using MediatR;

namespace TruckService.Api.Features.TruckTrailers.Update;

public sealed record UpdateTruckTrailerCommand(
    Guid Id,
    Guid TruckId,
    string TrailerNumber,
    string? TrailerType,
    string? Capacity
) : IRequest<UpdateTruckTrailerResult>;

public sealed record UpdateTruckTrailerResult(Guid Id, Guid TruckId, string TrailerNumber, string? TrailerType);
