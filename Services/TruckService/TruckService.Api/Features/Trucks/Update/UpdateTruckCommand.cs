using MediatR;

namespace TruckService.Api.Features.Trucks.Update;

public sealed record UpdateTruckCommand(
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
    DateTime? AcquisitionDate
) : IRequest<UpdateTruckResult>;

public sealed record UpdateTruckResult(Guid Id, string PlateNumber);
