using MediatR;

namespace TripService.Api.Features.Trips.UpdateDriverPay;

public sealed record UpdateDriverPayCommand(
    Guid TripId,
    decimal DriverPay
) : IRequest<UpdateDriverPayResult?>;

public sealed record UpdateDriverPayResult(Guid TripId, decimal DriverPay, DateTime UpdatedAt);
