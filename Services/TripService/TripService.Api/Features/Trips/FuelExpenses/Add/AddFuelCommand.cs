using MediatR;

namespace TripService.Api.Features.Trips.FuelExpenses.Add;

public sealed record AddFuelCommand(
    Guid TripId,
    string FuelType,
    decimal Liters,
    decimal PricePerLiter,
    string? Station
) : IRequest<AddFuelResult?>;

public sealed record AddFuelResult(
    Guid Id,
    Guid TripId,
    string FuelType,
    decimal Liters,
    decimal PricePerLiter,
    decimal Amount,
    string? Station,
    DateTime CreatedAt
);
