namespace Shared.Kernel.Events;

/// <summary>
/// Published by TripService when a trip is closed.
/// Consumed by ReportService to build report snapshots.
/// Topic: trip.closed
/// </summary>
public sealed record TripClosedEvent(
    Guid TripId,
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
    int Month,
    int Year,
    DateTime ClosedAt,

    // ← add full details here
    IEnumerable<TripClosedExpenseItem> Expenses,
    IEnumerable<TripClosedHelperItem> Helpers,
    IEnumerable<TripClosedFuelItem> FuelExpenses,
    IEnumerable<TripClosedSparePartItem> SpareParts
);