using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemesys.Migrations
{
    public partial class AddedFKsToReportTakeTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Reporter_reporteridNum",
                table: "Report");
/*
            migrationBuilder.DropTable(
                name: "CreateEditReportViewModel");

            migrationBuilder.DropTable(
                name: "ReportViewModel");*/

            migrationBuilder.AlterColumn<int>(
                name: "reporteridNum",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "investidationId",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Reporter_reporteridNum",
                table: "Report",
                column: "reporteridNum",
                principalTable: "Reporter",
                principalColumn: "idNum",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Reporter_reporteridNum",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "investidationId",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "reporteridNum",
                table: "Report",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CreateEditReportViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateEditReportViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    upvotes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportViewModel", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Reporter_reporteridNum",
                table: "Report",
                column: "reporteridNum",
                principalTable: "Reporter",
                principalColumn: "idNum",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
