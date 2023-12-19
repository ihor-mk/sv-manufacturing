using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunVita.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProductivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Productivity",
                table: "DoneTasks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 18, 17, 6, 5, 789, DateTimeKind.Local).AddTicks(9844));

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 18, 17, 6, 5, 789, DateTimeKind.Local).AddTicks(9856));

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 18, 17, 6, 5, 789, DateTimeKind.Local).AddTicks(9861));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Productivity",
                table: "DoneTasks");

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 18, 16, 14, 26, 993, DateTimeKind.Local).AddTicks(5257));

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 18, 16, 14, 26, 993, DateTimeKind.Local).AddTicks(5265));

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 18, 16, 14, 26, 993, DateTimeKind.Local).AddTicks(5270));
        }
    }
}
