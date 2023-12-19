using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunVita.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProductionLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ProductivityAvg",
                table: "ProductionLines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Code", "CreatedAt", "ProductivityAvg" },
                values: new object[] { "", new DateTime(2023, 12, 18, 16, 14, 26, 993, DateTimeKind.Local).AddTicks(5257), 1.0 });

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Code", "CreatedAt", "ProductivityAvg" },
                values: new object[] { "", new DateTime(2023, 12, 18, 16, 14, 26, 993, DateTimeKind.Local).AddTicks(5265), 1.0 });

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Code", "CreatedAt", "ProductivityAvg" },
                values: new object[] { "", new DateTime(2023, 12, 18, 16, 14, 26, 993, DateTimeKind.Local).AddTicks(5270), 1.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProductionLines");

            migrationBuilder.DropColumn(
                name: "ProductivityAvg",
                table: "ProductionLines");

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 8, 10, 16, 13, 617, DateTimeKind.Local).AddTicks(1319));

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 8, 10, 16, 13, 617, DateTimeKind.Local).AddTicks(1327));

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 8, 10, 16, 13, 617, DateTimeKind.Local).AddTicks(1332));
        }
    }
}
