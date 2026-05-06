using MediatR;

namespace TripService.Api.Features.Trips.Close;

public sealed record CloseTripCommand(Guid Id) : IRequest<CloseTripResult?>;

public sealed record CloseTripResult(Guid Id, string Status, DateTime UpdatedAt);
