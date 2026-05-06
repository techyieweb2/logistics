using MediatR;

namespace TripService.Api.Features.Trips.GetByTruck;

public sealed record GetTripsByTruckQuery(
    Guid TruckId,
    int Month,
    int Year
) : IRequest<GetTripsByTruckResult>;

public sealed record GetTripsByTruckResult(
    Guid TruckId,
    string TruckPlateNumber,
    int Month,
    int Year,
    int TotalTrips,
    IEnumerable<TruckTripItem> Trips
);

public sealed record TruckTripItem(
    Guid Id,
    Guid DispatchId,
    string DriverName,
    string CustomerName,
    string RouteName,
    string Origin,
    string Destination,
    decimal FreightCost,
    decimal DriverPay,
    string Status,
    DateTime CreatedAt,
    IEnumerable<TruckTripExpenseItem> Expenses,
    IEnumerable<TruckTripHelperItem> Helpers,
    IEnumerable<TruckTripFuelItem> FuelExpenses,
    IEnumerable<TruckTripSparePartItem> SpareParts
);

public sealed record TruckTripExpenseItem(
    string ExpenseType,
    string? Description,
    decimal Amount
);

public sealed record TruckTripHelperItem(
    string HelperName,
    decimal Pay
);

public sealed record TruckTripFuelItem(
    string FuelType,
    decimal Liters,
    decimal PricePerLiter,
    decimal Amount,
    string? Station
);

public sealed record TruckTripSparePartItem(
    string PartName,
    int Quantity,
    decimal UnitCost,
    decimal TotalCost
);