﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zamagon.Services.BackOffice.Database;

namespace Zamagon.Services.BackOffice.Database.Migrations.MySQL
{
    [DbContext(typeof(Db_MySQL))]
    partial class Db_MySQLModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("Zamagon.Model.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Zamagon.Model.TimeCard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<decimal>("HoursWorked")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("WorkDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("TimeCards");
                });

            modelBuilder.Entity("Zamagon.Model.TimeCard", b =>
                {
                    b.HasOne("Zamagon.Model.Employee", "Employee")
                        .WithMany("TimeCards")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Zamagon.Model.Employee", b =>
                {
                    b.Navigation("TimeCards");
                });
#pragma warning restore 612, 618
        }
    }
}
