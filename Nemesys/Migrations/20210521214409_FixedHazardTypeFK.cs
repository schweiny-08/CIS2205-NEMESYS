using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemesys.Migrations
{
    public partial class FixedHazardTypeFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_HazardType_hazardTypeId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "hazardId",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "hazardTypeId",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_HazardType_hazardTypeId",
                table: "Report",
                column: "hazardTypeId",
                principalTable: "HazardType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_HazardType_hazardTypeId",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "hazardTypeId",
                table: "Report",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "hazardId",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_HazardType_hazardTypeId",
                table: "Report",
                column: "hazardTypeId",
                principalTable: "HazardType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
