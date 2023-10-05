﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VsitPrinter.Infrastructure;

namespace VsitPrinter.Migrations
{
    [DbContext(typeof(PrinterDbContext))]
    partial class PrinterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("VsitPrinter.Infrastructure.Entities.PrinterDevice", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeviceName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("PrinterName");

                    b.Property<DateTime?>("RegisterDate");

                    b.Property<DateTime?>("ShutdownDate");

                    b.HasKey("Id");

                    b.ToTable("PrinterDevices");
                });

            modelBuilder.Entity("VsitPrinter.Infrastructure.Entities.PrinterJobExecuted", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ContractId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("FilePath");

                    b.Property<string>("FileType");

                    b.Property<int?>("FromPage");

                    b.Property<string>("GoogleCloudSetting");

                    b.Property<bool?>("IsDuplex");

                    b.Property<bool?>("IsHorizontal");

                    b.Property<string>("PrinterDeviceName");

                    b.Property<string>("PrinterName");

                    b.Property<string>("PrinterSetting");

                    b.Property<string>("PrinterType");

                    b.Property<int?>("ToPage");

                    b.HasKey("Id");

                    b.ToTable("PrinterJobExecuteds");
                });

            modelBuilder.Entity("VsitPrinter.Infrastructure.Entities.PrinterJobFailed", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ContractId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("ErrorMessage");

                    b.Property<string>("FilePath");

                    b.Property<string>("FileType");

                    b.Property<int?>("FromPage");

                    b.Property<string>("GoogleCloudSetting");

                    b.Property<bool?>("IsDuplex");

                    b.Property<bool?>("IsHorizontal");

                    b.Property<string>("PrinterDeviceName");

                    b.Property<string>("PrinterName");

                    b.Property<string>("PrinterSetting");

                    b.Property<string>("PrinterType");

                    b.Property<int?>("ToPage");

                    b.HasKey("Id");

                    b.ToTable("PrinterJobFaileds");
                });

            modelBuilder.Entity("VsitPrinter.Infrastructure.Entities.PrinterJobPending", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ContractId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("FilePath");

                    b.Property<string>("FileType");

                    b.Property<int?>("FromPage");

                    b.Property<string>("GoogleCloudSetting");

                    b.Property<bool?>("IsDuplex");

                    b.Property<bool?>("IsHorizontal");

                    b.Property<string>("PrinterDeviceName");

                    b.Property<string>("PrinterName");

                    b.Property<string>("PrinterProcessingId");

                    b.Property<string>("PrinterSetting");

                    b.Property<string>("PrinterType");

                    b.Property<int?>("ToPage");

                    b.HasKey("Id");

                    b.ToTable("PrinterJobPendings");
                });
#pragma warning restore 612, 618
        }
    }
}
