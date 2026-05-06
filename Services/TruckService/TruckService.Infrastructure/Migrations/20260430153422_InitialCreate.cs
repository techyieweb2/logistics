using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    ChassisNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    EngineNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    YearModel = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TruckType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    GrossVehicleWeight = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AcquisitionDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckDocuments_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TruckMaintenanceRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaintenanceType = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    OdometerReading = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServicedBy = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NextServiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckMaintenanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckMaintenanceRecords_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TruckStatusHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Notes = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckStatusHistory_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TruckTrailers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrailerNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TrailerType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckTrailers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckTrailers_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TruckDocuments_TruckId",
                table: "TruckDocuments",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckMaintenanceRecords_TruckId",
                table: "TruckMaintenanceRecords",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ChassisNumber",
                table: "Trucks",
                column: "ChassisNumber",
                unique: true,
                filter: "[ChassisNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_PlateNumber",
                table: "Trucks",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TruckStatusHistory_TruckId",
                table: "TruckStatusHistory",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckTrailers_TrailerNumber",
                table: "TruckTrailers",
                column: "TrailerNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TruckTrailers_TruckId",
                table: "TruckTrailers",
                column: "TruckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TruckDocuments");

            migrationBuilder.DropTable(
                name: "TruckMaintenanceRecords");

            migrationBuilder.DropTable(
                name: "TruckStatusHistory");

            migrationBuilder.DropTable(
                name: "TruckTrailers");

            migrationBuilder.DropTable(
                name: "Trucks");
        }
    }
}
