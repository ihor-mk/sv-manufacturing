using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunVita.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkDayField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DayPart",
                table: "DoneTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkDay",
                table: "DoneTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayPart",
                table: "DoneTasks");

            migrationBuilder.DropColumn(
                name: "WorkDay",
                table: "DoneTasks");

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 30, 14, 17, 38, 986, DateTimeKind.Local).AddTicks(5947));

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 30, 14, 17, 38, 986, DateTimeKind.Local).AddTicks(5955));

            migrationBuilder.UpdateData(
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 30, 14, 17, 38, 986, DateTimeKind.Local).AddTicks(5960));
        }
    }
}
