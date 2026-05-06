using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripReportSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DispatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckPlateNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RouteName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    FreightCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriverPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalHelperPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalFuelCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTollFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOtherExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSparePartsCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpensesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HelpersJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelExpensesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SparePartsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripReportSnapshots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripReportSnapshots_TripId",
                table: "TripReportSnapshots",
                column: "TripId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripReportSnapshots_TruckId_Month_Year",
                table: "TripReportSnapshots",
                columns: new[] { "TruckId", "Month", "Year" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripReportSnapshots");
        }
    }
}
