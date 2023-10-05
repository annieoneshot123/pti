using Microsoft.EntityFrameworkCore.Migrations;

namespace VsitPrinter.Migrations
{
    public partial class UpdatePrinterDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromPage",
                table: "PrinterJobPendings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplex",
                table: "PrinterJobPendings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHorizontal",
                table: "PrinterJobPendings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToPage",
                table: "PrinterJobPendings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FromPage",
                table: "PrinterJobFaileds",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplex",
                table: "PrinterJobFaileds",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHorizontal",
                table: "PrinterJobFaileds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToPage",
                table: "PrinterJobFaileds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FromPage",
                table: "PrinterJobExecuteds",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplex",
                table: "PrinterJobExecuteds",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHorizontal",
                table: "PrinterJobExecuteds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToPage",
                table: "PrinterJobExecuteds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromPage",
                table: "PrinterJobPendings");

            migrationBuilder.DropColumn(
                name: "IsDuplex",
                table: "PrinterJobPendings");

            migrationBuilder.DropColumn(
                name: "IsHorizontal",
                table: "PrinterJobPendings");

            migrationBuilder.DropColumn(
                name: "ToPage",
                table: "PrinterJobPendings");

            migrationBuilder.DropColumn(
                name: "FromPage",
                table: "PrinterJobFaileds");

            migrationBuilder.DropColumn(
                name: "IsDuplex",
                table: "PrinterJobFaileds");

            migrationBuilder.DropColumn(
                name: "IsHorizontal",
                table: "PrinterJobFaileds");

            migrationBuilder.DropColumn(
                name: "ToPage",
                table: "PrinterJobFaileds");

            migrationBuilder.DropColumn(
                name: "FromPage",
                table: "PrinterJobExecuteds");

            migrationBuilder.DropColumn(
                name: "IsDuplex",
                table: "PrinterJobExecuteds");

            migrationBuilder.DropColumn(
                name: "IsHorizontal",
                table: "PrinterJobExecuteds");

            migrationBuilder.DropColumn(
                name: "ToPage",
                table: "PrinterJobExecuteds");
        }
    }
}
