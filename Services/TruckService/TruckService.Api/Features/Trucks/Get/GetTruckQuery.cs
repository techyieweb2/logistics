// ─── Query ───────────────────────────────────────────────────────────────────
using MediatR;

namespace TruckService.Api.Features.Trucks.Get;

public sealed record GetTruckQuery(Guid Id) : IRequest<GetTruckResult>;

public sealed record GetTruckResult(
    Guid Id,
    string PlateNumber,
    string? ChassisNumber,
    string? EngineNumber,
    string? Brand,
    string? Model,
    int? YearModel,
    string? Color,
    string? TruckType,
    string? GrossVehicleWeight,
    string? Capacity,
    string? FuelType,
    string Status,
    DateTime? AcquisitionDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
