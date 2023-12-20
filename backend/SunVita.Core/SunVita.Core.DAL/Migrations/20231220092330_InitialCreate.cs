using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SunVita.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nomenclatures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomenclatureInBox = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomenclatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductivityAvg = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoneTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StringNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomenclatureId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Productivity = table.Column<double>(type: "float", nullable: false),
                    TeamTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionLineId = table.Column<long>(type: "bigint", nullable: false),
                    WorkDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayPart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoneTasks_Nomenclatures_NomenclatureId",
                        column: x => x.NomenclatureId,
                        principalTable: "Nomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoneTasks_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoneTaskEmployee",
                columns: table => new
                {
                    DoneTaskId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneTaskEmployee", x => new { x.DoneTaskId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_DoneTaskEmployee_DoneTasks_DoneTaskId",
                        column: x => x.DoneTaskId,
                        principalTable: "DoneTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoneTaskEmployee_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductionLines",
                columns: new[] { "Id", "Code", "CreatedAt", "IpAddress", "ProductivityAvg", "Title" },
                values: new object[,]
                {
                    { 1L, "000000040", new DateTime(2023, 12, 20, 11, 23, 30, 687, DateTimeKind.Local).AddTicks(2510), "", 26.800000000000001, "Цех №1 (Лінія 1)" },
                    { 2L, "000000009", new DateTime(2023, 12, 20, 11, 23, 30, 687, DateTimeKind.Local).AddTicks(2561), "10.61.2.22", 44.600000000000001, "Цех №2 (Лінія 1)" },
                    { 3L, "000000010", new DateTime(2023, 12, 20, 11, 23, 30, 687, DateTimeKind.Local).AddTicks(2567), "10.61.2.21", 50.0, "Цех №2 (Лінія 2)" },
                    { 4L, "000000008", new DateTime(2023, 12, 20, 11, 23, 30, 687, DateTimeKind.Local).AddTicks(2573), "", 51.700000000000003, "Цех №4 (Лінія 1, кросфолд 1)" },
                    { 5L, "000000047", new DateTime(2023, 12, 20, 11, 23, 30, 687, DateTimeKind.Local).AddTicks(2578), "10.61.2.23", 62.5, "Цех №5 (Лінія 1)" },
                    { 6L, "000000026", new DateTime(2023, 12, 20, 11, 23, 30, 687, DateTimeKind.Local).AddTicks(2587), "", 66.099999999999994, "Цех №5 (Лінія 2, кросфолд 2)" },
                    { 7L, "000000048", new DateTime(2023, 12, 20, 11, 23, 30, 687, DateTimeKind.Local).AddTicks(2593), "", 62.5, "Цех №5 (Лінія 3)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoneTaskEmployee_EmployeeId",
                table: "DoneTaskEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneTasks_NomenclatureId",
                table: "DoneTasks",
                column: "NomenclatureId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneTasks_ProductionLineId",
                table: "DoneTasks",
                column: "ProductionLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoneTaskEmployee");

            migrationBuilder.DropTable(
                name: "DoneTasks");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Nomenclatures");

            migrationBuilder.DropTable(
                name: "ProductionLines");
        }
    }
}
