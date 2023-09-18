using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DutchTreatHW.Migrations
{
    /// <inheritdoc />
    public partial class SeedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "OrderNumber" },
                values: new object[] { 1, new DateTime(2023, 9, 6, 20, 58, 43, 791, DateTimeKind.Utc).AddTicks(4196), "12345" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
