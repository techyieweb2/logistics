using MediatR;
using Microsoft.EntityFrameworkCore;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.Trucks.Update;

public sealed class UpdateTruckHandler : IRequestHandler<UpdateTruckCommand, UpdateTruckResult>
{
    private readonly TruckDbContext _context;

    public UpdateTruckHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateTruckResult> Handle(UpdateTruckCommand command, CancellationToken cancellationToken)
    {
        var truck = await _context.Trucks
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (truck is null)
            throw new KeyNotFoundException($"Truck with ID '{command.Id}' was not found.");

        truck.PlateNumber        = command.PlateNumber;
        truck.ChassisNumber      = command.ChassisNumber;
        truck.EngineNumber       = command.EngineNumber;
        truck.Brand              = command.Brand;
        truck.Model              = command.Model;
        truck.YearModel          = command.YearModel;
        truck.Color              = command.Color;
        truck.TruckType          = command.TruckType;
        truck.GrossVehicleWeight = command.GrossVehicleWeight;
        truck.Capacity           = command.Capacity;
        truck.FuelType           = command.FuelType;
        truck.AcquisitionDate    = command.AcquisitionDate;
        truck.UpdatedAt          = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateTruckResult(truck.Id, truck.PlateNumber);
    }
}
