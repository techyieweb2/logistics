using MediatR;

namespace TruckService.Api.Features.Trucks.Create;

public sealed record CreateTruckCommand(
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
    DateTime? AcquisitionDate
) : IRequest<CreateTruckResult>;

public sealed record CreateTruckResult(Guid Id, string PlateNumber);
