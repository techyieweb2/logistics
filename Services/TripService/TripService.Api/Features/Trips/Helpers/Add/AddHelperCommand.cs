using MediatR;

namespace TripService.Api.Features.Trips.Helpers.Add;

public sealed record AddHelperCommand(
    Guid TripId,
    string HelperName,
    decimal Pay
) : IRequest<AddHelperResult?>;

public sealed record AddHelperResult(
    Guid Id,
    Guid TripId,
    string HelperName,
    decimal Pay,
    DateTime CreatedAt
);
