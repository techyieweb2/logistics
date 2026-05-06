using Shared.Kernel.Common;
using System.Text.Json;

namespace ReportService.Domain.Entities;

public class TripReportSnapshot : BaseEntity
{
    public Guid TripId { get; set; }
    public Guid DispatchId { get; set; }

    // Truck
    public Guid TruckId { get; set; }
    public string TruckPlateNumber { get; set; } = string.Empty;

    // Driver
    public Guid DriverId { get; set; }
    public string DriverName { get; set; } = string.Empty;

    // Customer
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    // Route
    public string RouteName { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;

    // Period
    public int Month { get; set; }
    public int Year { get; set; }

    // Financials
    public decimal FreightCost { get; set; }
    public decimal DriverPay { get; set; }
    public decimal TotalHelperPay { get; set; }
    public decimal TotalFuelCost { get; set; }
    public decimal TotalTollFee { get; set; }
    public decimal TotalOtherExpenses { get; set; }
    public decimal TotalSparePartsCost { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetAmount { get; set; }

    // Full detail as JSON for report line items
    public string ExpensesJson { get; set; } = "[]";
    public string HelpersJson { get; set; } = "[]";
    public string FuelExpensesJson { get; set; } = "[]";
    public string SparePartsJson { get; set; } = "[]";

    public DateTime ClosedAt { get; set; }
}