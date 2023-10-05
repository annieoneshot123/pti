using Microsoft.EntityFrameworkCore.Migrations;

namespace VsitPrinter.Migrations
{
    public partial class AddParamFromPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "PrinterJobPendings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "PrinterJobFaileds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "PrinterJobExecuteds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "PrinterJobPendings");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "PrinterJobFaileds");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "PrinterJobExecuteds");
        }
    }
}
