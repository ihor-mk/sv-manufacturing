﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SunVita.Core.DAL.Context;

#nullable disable

namespace SunVita.Core.DAL.Migrations
{
    [DbContext(typeof(SunVitaCoreContext))]
    partial class SunVitaCoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SunVita.Core.DAL.Entities.DoneTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DayPart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("NomenclatureId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductionLineId")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("StringNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WorkDay")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NomenclatureId");

                    b.HasIndex("ProductionLineId");

                    b.ToTable("DoneTasks");
                });

            modelBuilder.Entity("SunVita.Core.DAL.Entities.DoneTaskEmployee", b =>
                {
                    b.Property<long>("DoneTaskId")
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.HasKey("DoneTaskId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("DoneTaskEmployee");
                });

            modelBuilder.Entity("SunVita.Core.DAL.Entities.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SunVita.Core.DAL.Entities.Nomenclature", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("NomenclatureInBox")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Nomenclatures");
                });

            modelBuilder.Entity("SunVita.Core.DAL.Entities.ProductionLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductionLines");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2023, 12, 8, 10, 16, 13, 617, DateTimeKind.Local).AddTicks(1319),
                            IpAddress = "10.61.2.21",
                            Title = "Цех №2  (Лінія1)"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2023, 12, 8, 10, 16, 13, 617, DateTimeKind.Local).AddTicks(1327),
                            IpAddress = "10.61.2.22",
                            Title = "Цех №2 (Лінія 2)"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2023, 12, 8, 10, 16, 13, 617, DateTimeKind.Local).AddTicks(1332),
                            IpAddress = "10.61.2.23",
                            Title = "Цех №5"
                        });
                });

            modelBuilder.Entity("SunVita.Core.DAL.Entities.DoneTask", b =>
                {
                    b.HasOne("SunVita.Core.DAL.Entities.Nomenclature", "Nomenclature")
                        .WithMany()
                        .HasForeignKey("NomenclatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SunVita.Core.DAL.Entities.ProductionLine", "ProductionLine")
                        .WithMany()
                        .HasForeignKey("ProductionLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nomenclature");

                    b.Navigation("ProductionLine");
                });

            modelBuilder.Entity("SunVita.Core.DAL.Entities.DoneTaskEmployee", b =>
                {
                    b.HasOne("SunVita.Core.DAL.Entities.DoneTask", null)
                        .WithMany()
                        .HasForeignKey("DoneTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SunVita.Core.DAL.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
