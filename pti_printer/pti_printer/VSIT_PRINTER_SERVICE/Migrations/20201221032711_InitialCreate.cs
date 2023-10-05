using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VsitPrinter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrinterDevices",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DeviceName = table.Column<string>(nullable: true),
                    PrinterName = table.Column<string>(nullable: true),
                    RegisterDate = table.Column<DateTime>(nullable: true),
                    ShutdownDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrinterJobExecuteds",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    PrinterName = table.Column<string>(nullable: true),
                    PrinterSetting = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    PrinterType = table.Column<string>(nullable: true),
                    GoogleCloudSetting = table.Column<string>(nullable: true),
                    ContractId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterJobExecuteds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrinterJobFaileds",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    PrinterName = table.Column<string>(nullable: true),
                    PrinterSetting = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    PrinterType = table.Column<string>(nullable: true),
                    GoogleCloudSetting = table.Column<string>(nullable: true),
                    ContractId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterJobFaileds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrinterJobPendings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    PrinterName = table.Column<string>(nullable: true),
                    PrinterSetting = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    PrinterType = table.Column<string>(nullable: true),
                    GoogleCloudSetting = table.Column<string>(nullable: true),
                    ContractId = table.Column<long>(nullable: false),
                    PrinterProcessingId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterJobPendings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrinterDevices");

            migrationBuilder.DropTable(
                name: "PrinterJobExecuteds");

            migrationBuilder.DropTable(
                name: "PrinterJobFaileds");

            migrationBuilder.DropTable(
                name: "PrinterJobPendings");
        }
    }
}
