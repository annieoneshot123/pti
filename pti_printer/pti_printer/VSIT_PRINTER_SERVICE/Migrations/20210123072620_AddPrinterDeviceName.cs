using Microsoft.EntityFrameworkCore.Migrations;

namespace VsitPrinter.Migrations
{
    public partial class AddPrinterDeviceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrinterDeviceName",
                table: "PrinterJobPendings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrinterDeviceName",
                table: "PrinterJobFaileds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrinterDeviceName",
                table: "PrinterJobExecuteds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrinterDeviceName",
                table: "PrinterJobPendings");

            migrationBuilder.DropColumn(
                name: "PrinterDeviceName",
                table: "PrinterJobFaileds");

            migrationBuilder.DropColumn(
                name: "PrinterDeviceName",
                table: "PrinterJobExecuteds");
        }
    }
}
