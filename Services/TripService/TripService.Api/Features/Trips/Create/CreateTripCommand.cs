using MediatR;

namespace TripService.Api.Features.Trips.Create;

public sealed record CreateTripCommand(
    Guid DispatchId,

    // Truck info — from UI dispatch selection
    Guid TruckId,
    string TruckPlateNumber,

    // Driver info — from UI dispatch selection
    Guid DriverId,
    string DriverName,

    // Customer info — from UI dispatch selection
    Guid CustomerId,
    string CustomerName,

    // Route info — from UI dispatch selection
    string RouteName,
    string Origin,
    string Destination,

    decimal FreightCost,
    decimal DriverPay,
    string? Notes,
    IEnumerable<CreateTripExpenseItem>? Expenses,
    IEnumerable<CreateTripHelperItem>? Helpers,
    IEnumerable<CreateTripFuelItem>? FuelExpenses,
    IEnumerable<CreateTripSparePartItem>? SpareParts
) : IRequest<CreateTripResult>;

public sealed record CreateTripExpenseItem(
    string ExpenseType,             // TollFee, Other
    string? Description,
    decimal Amount
);

public sealed record CreateTripHelperItem(
    string HelperName,
    decimal Pay
);

public sealed record CreateTripFuelItem(
    string FuelType,                // Diesel, Gas
    decimal Liters,
    decimal PricePerLiter,
    string? Station
);

public sealed record CreateTripSparePartItem(
    string PartName,
    int Quantity,
    decimal UnitCost
);

public sealed record CreateTripResult(
    Guid Id,
    Guid DispatchId,
    string TruckPlateNumber,
    string DriverName,
    string CustomerName,
    string RouteName,
    string Origin,
    string Destination,
    decimal FreightCost,
    decimal DriverPay,
    string Status
);
