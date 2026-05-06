using MediatR;

namespace TripService.Api.Features.Trips.Get;

public sealed record GetTripQuery(Guid Id) : IRequest<GetTripResult?>;

public sealed record GetTripResult(
    Guid Id,
    Guid DispatchId,
    Guid TruckId,
    string TruckPlateNumber,
    Guid DriverId,
    string DriverName,
    Guid CustomerId,
    string CustomerName,
    string RouteName,
    string Origin,
    string Destination,
    decimal FreightCost,
    decimal DriverPay,
    string Status,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<TripExpenseResult> Expenses,
    IEnumerable<TripHelperResult> Helpers,
    IEnumerable<TripFuelResult> FuelExpenses,
    IEnumerable<TripSparePartResult> SpareParts
);

public sealed record TripExpenseResult(
    Guid Id,
    string ExpenseType,
    string? Description,
    decimal Amount
);

public sealed record TripHelperResult(
    Guid Id,
    string HelperName,
    decimal Pay
);

public sealed record TripFuelResult(
    Guid Id,
    string FuelType,
    decimal Liters,
    decimal PricePerLiter,
    decimal Amount,
    string? Station
);

public sealed record TripSparePartResult(
    Guid Id,
    string PartName,
    int Quantity,
    decimal UnitCost,
    decimal TotalCost
);