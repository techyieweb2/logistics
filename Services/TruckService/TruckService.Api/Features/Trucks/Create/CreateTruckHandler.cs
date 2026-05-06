using MediatR;
using TruckService.Domain.Entities;
using TruckService.Domain.Enums;
using TruckService.Infrastructure.Persistence;

namespace TruckService.Api.Features.Trucks.Create;

public sealed class CreateTruckHandler : IRequestHandler<CreateTruckCommand, CreateTruckResult>
{
    private readonly TruckDbContext _context;

    public CreateTruckHandler(TruckDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTruckResult> Handle(CreateTruckCommand command, CancellationToken cancellationToken)
    {
        var truck = new Truck
        {
            PlateNumber       = command.PlateNumber,
            ChassisNumber     = command.ChassisNumber,
            EngineNumber      = command.EngineNumber,
            Brand             = command.Brand,
            Model             = command.Model,
            YearModel         = command.YearModel,
            Color             = command.Color,
            TruckType         = command.TruckType,
            GrossVehicleWeight = command.GrossVehicleWeight,
            Capacity          = command.Capacity,
            FuelType          = command.FuelType,
            Status            = TruckStatus.Available,
            AcquisitionDate   = command.AcquisitionDate,
            CreatedAt         = DateTime.UtcNow
        };

        _context.Trucks.Add(truck);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTruckResult(truck.Id, truck.PlateNumber);
    }
}
