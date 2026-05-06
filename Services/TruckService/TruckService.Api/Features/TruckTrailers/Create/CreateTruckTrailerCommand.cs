using MediatR;

namespace TruckService.Api.Features.TruckTrailers.Create;

public sealed record CreateTruckTrailerCommand(
    Guid TruckId,
    string TrailerNumber,
    string? TrailerType,
    string? Capacity
) : IRequest<CreateTruckTrailerResult>;

public sealed record CreateTruckTrailerResult(Guid Id, Guid TruckId, string TrailerNumber, string? TrailerType);
