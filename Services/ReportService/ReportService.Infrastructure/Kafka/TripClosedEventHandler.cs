using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Persistence;
using Shared.Kernel.Events;

namespace ReportService.Infrastructure.Kafka;

public sealed class TripClosedEventHandler
{
    private readonly ReportDbContext _context;
    private readonly ILogger<TripClosedEventHandler> _logger;

    public TripClosedEventHandler(
        ReportDbContext context,
        ILogger<TripClosedEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task HandleAsync(TripClosedEvent tripEvent, CancellationToken cancellationToken)
    {
        // Idempotency check — skip if snapshot already exists
        var exists = await _context.TripReportSnapshots
            .AnyAsync(s => s.TripId == tripEvent.TripId, cancellationToken);

        if (exists)
        {
            _logger.LogWarning(
                "Snapshot for TripId {TripId} already exists. Skipping.",
                tripEvent.TripId);
            return;
        }

        // Compute aggregates directly from event data — no HTTP call needed
        var totalHelperPay = tripEvent.Helpers.Sum(h => h.Pay);
        var totalFuelCost = tripEvent.FuelExpenses.Sum(f => f.Amount);
        var totalTollFee = tripEvent.Expenses
                                    .Where(e => e.ExpenseType == "TollFee")
                                    .Sum(e => e.Amount);
        var totalOtherExpenses = tripEvent.Expenses
                                    .Where(e => e.ExpenseType == "Other")
                                    .Sum(e => e.Amount);
        var totalSparePartsCost = tripEvent.SpareParts.Sum(s => s.TotalCost);

        var totalExpenses = tripEvent.DriverPay
            + totalHelperPay
            + totalFuelCost
            + totalTollFee
            + totalOtherExpenses
            + totalSparePartsCost;

        var netAmount = tripEvent.FreightCost - totalExpenses;

        var snapshot = new TripReportSnapshot
        {
            TripId = tripEvent.TripId,
            DispatchId = tripEvent.DispatchId,
            TruckId = tripEvent.TruckId,
            TruckPlateNumber = tripEvent.TruckPlateNumber,
            DriverId = tripEvent.DriverId,
            DriverName = tripEvent.DriverName,
            CustomerId = tripEvent.CustomerId,
            CustomerName = tripEvent.CustomerName,
            RouteName = tripEvent.RouteName,
            Origin = tripEvent.Origin,
            Destination = tripEvent.Destination,
            Month = tripEvent.Month,
            Year = tripEvent.Year,
            FreightCost = tripEvent.FreightCost,
            DriverPay = tripEvent.DriverPay,
            TotalHelperPay = totalHelperPay,
            TotalFuelCost = totalFuelCost,
            TotalTollFee = totalTollFee,
            TotalOtherExpenses = totalOtherExpenses,
            TotalSparePartsCost = totalSparePartsCost,
            TotalExpenses = totalExpenses,
            NetAmount = netAmount,

            // Store full line items as JSON for PDF/Excel detail rows
            ExpensesJson = JsonSerializer.Serialize(tripEvent.Expenses),
            HelpersJson = JsonSerializer.Serialize(tripEvent.Helpers),
            FuelExpensesJson = JsonSerializer.Serialize(tripEvent.FuelExpenses),
            SparePartsJson = JsonSerializer.Serialize(tripEvent.SpareParts),

            ClosedAt = tripEvent.ClosedAt,
            CreatedAt = DateTime.UtcNow
        };

        _context.TripReportSnapshots.Add(snapshot);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Snapshot created for TripId {TripId} — Truck {Plate} — {Month}/{Year} — Net: {Net}",
            tripEvent.TripId,
            tripEvent.TruckPlateNumber,
            tripEvent.Month,
            tripEvent.Year,
            netAmount);
    }
}