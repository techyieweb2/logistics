using System.Text.Json;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ReportService.Infrastructure.Kafka;
using ReportService.Infrastructure.Persistence;

namespace ReportService.Api.Features.Reports.GenerateTruckMonthlyReport;

public sealed class GenerateTruckMonthlyReportHandler
    : IRequestHandler<GenerateTruckMonthlyReportQuery, ReportFileResult>
{
    private readonly ReportDbContext _context;

    public GenerateTruckMonthlyReportHandler(ReportDbContext context)
    {
        _context = context;
    }

    public async Task<ReportFileResult> Handle(
        GenerateTruckMonthlyReportQuery query,
        CancellationToken cancellationToken)
    {
        var snapshots = await _context.TripReportSnapshots
            .AsNoTracking()
            .Where(s =>
                s.TruckId == query.TruckId &&
                s.Month   == query.Month &&
                s.Year    == query.Year)
            .OrderBy(s => s.ClosedAt)
            .ToListAsync(cancellationToken);

        if (!snapshots.Any())
            throw new InvalidOperationException("No trips found for this truck in the specified period.");

        var monthName    = new DateTime(query.Year, query.Month, 1).ToString("MMMM yyyy");
        var plateNumber  = snapshots.First().TruckPlateNumber;

        return query.Format.ToLower() switch
        {
            "pdf"   => GeneratePdf(snapshots, plateNumber, monthName),
            "excel" => GenerateExcel(snapshots, plateNumber, monthName),
            _       => throw new ArgumentException("Format must be 'pdf' or 'excel'.")
        };
    }

    // ─── PDF Generation ────────────────────────────────────────────────────────

    private static ReportFileResult GeneratePdf(
        List<ReportService.Domain.Entities.TripReportSnapshot> snapshots,
        string plateNumber,
        string monthName)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(9));

                page.Header().Element(header =>
                {
                    header.Column(col =>
                    {
                        col.Item().Text("MONTHLY TRIP REPORT")
                            .FontSize(16).Bold().AlignCenter();
                        col.Item().Text($"Truck: {plateNumber}  |  Period: {monthName}")
                            .FontSize(11).AlignCenter();
                        col.Item().PaddingTop(5).LineHorizontal(1);
                    });
                });

                page.Content().PaddingTop(10).Column(col =>
                {
                    var tripNumber = 1;

                    foreach (var trip in snapshots)
                    {
                        var expenses   = JsonSerializer.Deserialize<List<TripExpenseDto>>(trip.ExpensesJson) ?? [];
                        var helpers    = JsonSerializer.Deserialize<List<TripHelperDto>>(trip.HelpersJson) ?? [];
                        var fuels      = JsonSerializer.Deserialize<List<TripFuelDto>>(trip.FuelExpensesJson) ?? [];
                        var spareParts = JsonSerializer.Deserialize<List<TripSparePartDto>>(trip.SparePartsJson) ?? [];

                        // Trip header
                        col.Item().PaddingTop(8).Background("#f0f0f0").Padding(5).Row(row =>
                        {
                            row.RelativeItem().Text($"TRIP {tripNumber}  |  {trip.ClosedAt:MM/dd/yyyy}").Bold();
                            row.RelativeItem().Text($"Driver: {trip.DriverName}").AlignRight();
                        });

                        col.Item().Padding(5).Column(inner =>
                        {
                            inner.Item().Text($"Customer: {trip.CustomerName}  |  Route: {trip.Origin} → {trip.Destination}");

                            inner.Item().PaddingTop(4).Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(3);
                                    c.RelativeColumn(1);
                                });

                                // Freight
                                table.Cell().Text("Freight Cost").Bold();
                                table.Cell().Text($"₱ {trip.FreightCost:N2}").AlignRight().Bold();

                                // Driver Pay
                                table.Cell().Text("  Driver Pay");
                                table.Cell().Text($"₱ {trip.DriverPay:N2}").AlignRight();

                                // Helpers
                                foreach (var h in helpers)
                                {
                                    table.Cell().Text($"  Helper: {h.HelperName}");
                                    table.Cell().Text($"₱ {h.Pay:N2}").AlignRight();
                                }

                                // Fuel
                                foreach (var f in fuels)
                                {
                                    table.Cell().Text($"  {f.FuelType} {f.Liters}L @ ₱{f.PricePerLiter:N2}{(f.Station != null ? $" ({f.Station})" : "")}");
                                    table.Cell().Text($"₱ {f.Amount:N2}").AlignRight();
                                }

                                // Expenses
                                foreach (var e in expenses)
                                {
                                    table.Cell().Text($"  {e.ExpenseType}{(e.Description != null ? $": {e.Description}" : "")}");
                                    table.Cell().Text($"₱ {e.Amount:N2}").AlignRight();
                                }

                                // Spare Parts
                                foreach (var s in spareParts)
                                {
                                    table.Cell().Text($"  Spare Part: {s.PartName} x{s.Quantity} @ ₱{s.UnitCost:N2}");
                                    table.Cell().Text($"₱ {s.TotalCost:N2}").AlignRight();
                                }

                                // Trip totals
                                table.Cell().PaddingTop(4).Text("Total Expenses").Bold();
                                table.Cell().PaddingTop(4).Text($"₱ {trip.TotalExpenses:N2}").AlignRight().Bold();

                                table.Cell().Text("Trip Net").Bold();
                                table.Cell().Text($"₱ {trip.NetAmount:N2}").AlignRight().Bold();
                            });
                        });

                        col.Item().LineHorizontal(0.5f);
                        tripNumber++;
                    }

                    // Monthly Summary
                    col.Item().PaddingTop(15).Background("#1a1a2e").Padding(8).Text("MONTHLY SUMMARY")
                        .FontColor("#ffffff").Bold().FontSize(12);

                    col.Item().Padding(8).Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(3);
                            c.RelativeColumn(1);
                        });

                        table.Cell().Text($"Total Trips").Bold();
                        table.Cell().Text($"{snapshots.Count}").AlignRight().Bold();

                        table.Cell().PaddingTop(6).Text("TOTAL COLLECTION").Bold();
                        table.Cell().PaddingTop(6).Text($"₱ {snapshots.Sum(s => s.FreightCost):N2}").AlignRight().Bold();

                        table.Cell().PaddingTop(6).Text("TOTAL DEDUCTIONS").Bold();
                        table.Cell().Text("").AlignRight();

                        table.Cell().Text("  Total Driver Pay");
                        table.Cell().Text($"₱ {snapshots.Sum(s => s.DriverPay):N2}").AlignRight();

                        table.Cell().Text("  Total Helper Pay");
                        table.Cell().Text($"₱ {snapshots.Sum(s => s.TotalHelperPay):N2}").AlignRight();

                        table.Cell().Text("  Total Fuel");
                        table.Cell().Text($"₱ {snapshots.Sum(s => s.TotalFuelCost):N2}").AlignRight();

                        table.Cell().Text("  Total Toll & Other");
                        table.Cell().Text($"₱ {snapshots.Sum(s => s.TotalTollFee + s.TotalOtherExpenses):N2}").AlignRight();

                        table.Cell().Text("  Total Spare Parts");
                        table.Cell().Text($"₱ {snapshots.Sum(s => s.TotalSparePartsCost):N2}").AlignRight();

                        table.Cell().PaddingTop(4).Text("Total Deductions").Bold();
                        table.Cell().PaddingTop(4).Text($"₱ {snapshots.Sum(s => s.TotalExpenses):N2}").AlignRight().Bold();

                        table.Cell().PaddingTop(8).Text("NET AMOUNT PAYABLE").Bold().FontSize(12);
                        table.Cell().PaddingTop(8).Text($"₱ {snapshots.Sum(s => s.NetAmount):N2}").AlignRight().Bold().FontSize(12);
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Generated: ").FontSize(8);
                    text.Span(DateTime.Now.ToString("MM/dd/yyyy HH:mm")).FontSize(8);
                    text.Span("   Page ").FontSize(8);
                    text.CurrentPageNumber().FontSize(8);
                    text.Span(" of ").FontSize(8);
                    text.TotalPages().FontSize(8);
                });
            });
        });

        var bytes = pdf.GeneratePdf();
        return new ReportFileResult(
            bytes,
            "application/pdf",
            $"TruckReport_{plateNumber}_{monthName.Replace(" ", "_")}.pdf"
        );
    }

    // ─── Excel Generation ───────────────────────────────────────────────────────

    private static ReportFileResult GenerateExcel(
        List<ReportService.Domain.Entities.TripReportSnapshot> snapshots,
        string plateNumber,
        string monthName)
    {
        using var workbook = new XLWorkbook();

        // ── Sheet 1: Trip Details ──────────────────────────────────────────────
        var detailSheet = workbook.Worksheets.Add("Trip Details");
        var row = 1;

        detailSheet.Cell(row, 1).Value = $"Monthly Trip Report — Truck: {plateNumber} — {monthName}";
        detailSheet.Cell(row, 1).Style.Font.Bold = true;
        detailSheet.Cell(row, 1).Style.Font.FontSize = 14;
        detailSheet.Range(row, 1, row, 6).Merge();
        row += 2;

        // Headers
        string[] headers = ["#", "Date", "Driver", "Customer", "Route", "Freight Cost",
                             "Driver Pay", "Helper Pay", "Fuel Cost", "Toll/Other", "Spare Parts", "Total Expenses", "Net Amount"];

        for (int i = 0; i < headers.Length; i++)
        {
            detailSheet.Cell(row, i + 1).Value = headers[i];
            detailSheet.Cell(row, i + 1).Style.Font.Bold = true;
            detailSheet.Cell(row, i + 1).Style.Fill.BackgroundColor = XLColor.DarkBlue;
            detailSheet.Cell(row, i + 1).Style.Font.FontColor = XLColor.White;
        }
        row++;

        var tripNumber = 1;
        foreach (var trip in snapshots)
        {
            detailSheet.Cell(row, 1).Value  = tripNumber;
            detailSheet.Cell(row, 2).Value  = trip.ClosedAt.ToString("MM/dd/yyyy");
            detailSheet.Cell(row, 3).Value  = trip.DriverName;
            detailSheet.Cell(row, 4).Value  = trip.CustomerName;
            detailSheet.Cell(row, 5).Value  = $"{trip.Origin} → {trip.Destination}";
            detailSheet.Cell(row, 6).Value  = trip.FreightCost;
            detailSheet.Cell(row, 7).Value  = trip.DriverPay;
            detailSheet.Cell(row, 8).Value  = trip.TotalHelperPay;
            detailSheet.Cell(row, 9).Value  = trip.TotalFuelCost;
            detailSheet.Cell(row, 10).Value = trip.TotalTollFee + trip.TotalOtherExpenses;
            detailSheet.Cell(row, 11).Value = trip.TotalSparePartsCost;
            detailSheet.Cell(row, 12).Value = trip.TotalExpenses;
            detailSheet.Cell(row, 13).Value = trip.NetAmount;

            // Format as currency
            foreach (var col in new[] { 6, 7, 8, 9, 10, 11, 12, 13 })
                detailSheet.Cell(row, col).Style.NumberFormat.Format = "₱#,##0.00";

            row++;
            tripNumber++;
        }

        // Totals row
        row++;
        detailSheet.Cell(row, 5).Value = "TOTALS";
        detailSheet.Cell(row, 5).Style.Font.Bold = true;
        detailSheet.Cell(row, 6).Value  = snapshots.Sum(s => s.FreightCost);
        detailSheet.Cell(row, 7).Value  = snapshots.Sum(s => s.DriverPay);
        detailSheet.Cell(row, 8).Value  = snapshots.Sum(s => s.TotalHelperPay);
        detailSheet.Cell(row, 9).Value  = snapshots.Sum(s => s.TotalFuelCost);
        detailSheet.Cell(row, 10).Value = snapshots.Sum(s => s.TotalTollFee + s.TotalOtherExpenses);
        detailSheet.Cell(row, 11).Value = snapshots.Sum(s => s.TotalSparePartsCost);
        detailSheet.Cell(row, 12).Value = snapshots.Sum(s => s.TotalExpenses);
        detailSheet.Cell(row, 13).Value = snapshots.Sum(s => s.NetAmount);

        foreach (var col in new[] { 6, 7, 8, 9, 10, 11, 12, 13 })
        {
            detailSheet.Cell(row, col).Style.Font.Bold = true;
            detailSheet.Cell(row, col).Style.NumberFormat.Format = "₱#,##0.00";
            detailSheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
        }

        detailSheet.Columns().AdjustToContents();

        // ── Sheet 2: Monthly Summary ───────────────────────────────────────────
        var summarySheet = workbook.Worksheets.Add("Monthly Summary");
        var sRow = 1;

        summarySheet.Cell(sRow, 1).Value = "MONTHLY SUMMARY";
        summarySheet.Cell(sRow, 1).Style.Font.Bold = true;
        summarySheet.Cell(sRow, 1).Style.Font.FontSize = 14;
        sRow += 2;

        void AddSummaryRow(string label, decimal value, bool bold = false)
        {
            summarySheet.Cell(sRow, 1).Value = label;
            summarySheet.Cell(sRow, 2).Value = value;
            summarySheet.Cell(sRow, 2).Style.NumberFormat.Format = "₱#,##0.00";
            if (bold)
            {
                summarySheet.Cell(sRow, 1).Style.Font.Bold = true;
                summarySheet.Cell(sRow, 2).Style.Font.Bold = true;
            }
            sRow++;
        }

        summarySheet.Cell(sRow, 1).Value = "Total Trips";
        summarySheet.Cell(sRow, 2).Value = snapshots.Count;
        summarySheet.Cell(sRow, 1).Style.Font.Bold = true;
        summarySheet.Cell(sRow, 2).Style.Font.Bold = true;
        sRow += 2;

        AddSummaryRow("TOTAL FREIGHT COLLECTION", snapshots.Sum(s => s.FreightCost), true);
        sRow++;

        summarySheet.Cell(sRow, 1).Value = "TOTAL DEDUCTIONS";
        summarySheet.Cell(sRow, 1).Style.Font.Bold = true;
        sRow++;

        AddSummaryRow("  Total Driver Pay", snapshots.Sum(s => s.DriverPay));
        AddSummaryRow("  Total Helper Pay", snapshots.Sum(s => s.TotalHelperPay));
        AddSummaryRow("  Total Fuel", snapshots.Sum(s => s.TotalFuelCost));
        AddSummaryRow("  Total Toll & Other", snapshots.Sum(s => s.TotalTollFee + s.TotalOtherExpenses));
        AddSummaryRow("  Total Spare Parts", snapshots.Sum(s => s.TotalSparePartsCost));
        sRow++;
        AddSummaryRow("TOTAL DEDUCTIONS", snapshots.Sum(s => s.TotalExpenses), true);
        sRow++;
        AddSummaryRow("NET AMOUNT PAYABLE", snapshots.Sum(s => s.NetAmount), true);

        summarySheet.Cell(sRow - 1, 1).Style.Fill.BackgroundColor = XLColor.LightGreen;
        summarySheet.Cell(sRow - 1, 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
        summarySheet.Cell(sRow - 1, 2).Style.Font.FontSize = 12;

        summarySheet.Columns().AdjustToContents();

        // Export to bytes
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return new ReportFileResult(
            stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"TruckReport_{plateNumber}_{monthName.Replace(" ", "_")}.xlsx"
        );
    }
}

// Local DTOs for JSON deserialization
file sealed record TripExpenseDto(string ExpenseType, string? Description, decimal Amount);
file sealed record TripHelperDto(string HelperName, decimal Pay);
file sealed record TripFuelDto(string FuelType, decimal Liters, decimal PricePerLiter, decimal Amount, string? Station);
file sealed record TripSparePartDto(string PartName, int Quantity, decimal UnitCost, decimal TotalCost);
