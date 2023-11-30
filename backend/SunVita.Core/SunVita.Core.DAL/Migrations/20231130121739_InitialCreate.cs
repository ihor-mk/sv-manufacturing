﻿using System;
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
                    TeamTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionLineId = table.Column<long>(type: "bigint", nullable: false),
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
                columns: new[] { "Id", "CreatedAt", "IpAddress", "Title" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 11, 30, 14, 17, 38, 986, DateTimeKind.Local).AddTicks(5947), "10.61.2.21", "Цех №2  (Лінія1)" },
                    { 2L, new DateTime(2023, 11, 30, 14, 17, 38, 986, DateTimeKind.Local).AddTicks(5955), "10.61.2.22", "Цех №2 (Лінія 2)" },
                    { 3L, new DateTime(2023, 11, 30, 14, 17, 38, 986, DateTimeKind.Local).AddTicks(5960), "10.61.2.23", "Цех №5" }
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