using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExternalCustomerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Dispatches",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Dispatches");
        }
    }
}
