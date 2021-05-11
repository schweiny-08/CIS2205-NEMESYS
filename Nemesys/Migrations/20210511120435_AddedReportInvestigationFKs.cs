using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemesys.Migrations
{
    public partial class AddedReportInvestigationFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investigation_Investigator_investigatoridNum",
                table: "Investigation");

            migrationBuilder.DropForeignKey(
                name: "FK_Investigation_Report_reportidNum",
                table: "Investigation");

            migrationBuilder.DropIndex(
                name: "IX_Investigation_investigatoridNum",
                table: "Investigation");

            migrationBuilder.DropIndex(
                name: "IX_Investigation_reportidNum",
                table: "Investigation");

            migrationBuilder.DropColumn(
                name: "investigatoridNum",
                table: "Investigation");

            migrationBuilder.DropColumn(
                name: "reportidNum",
                table: "Investigation");

            migrationBuilder.AddColumn<int>(
                name: "investigatorId",
                table: "Investigation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "reportId",
                table: "Investigation",
                type: "int",
                nullable: false,
                defaultValue: 0);
/*
            migrationBuilder.CreateTable(
                name: "CreateEditReportViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateEditReportViewModel", x => x.Id);
                });*/

            migrationBuilder.CreateIndex(
                name: "IX_Investigation_investigatorId",
                table: "Investigation",
                column: "investigatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Investigation_reportId",
                table: "Investigation",
                column: "reportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Investigation_Investigator_investigatorId",
                table: "Investigation",
                column: "investigatorId",
                principalTable: "Investigator",
                principalColumn: "idNum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Investigation_Report_reportId",
                table: "Investigation",
                column: "reportId",
                principalTable: "Report",
                principalColumn: "idNum",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investigation_Investigator_investigatorId",
                table: "Investigation");

            migrationBuilder.DropForeignKey(
                name: "FK_Investigation_Report_reportId",
                table: "Investigation");

            migrationBuilder.DropTable(
                name: "CreateEditReportViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Investigation_investigatorId",
                table: "Investigation");

            migrationBuilder.DropIndex(
                name: "IX_Investigation_reportId",
                table: "Investigation");

            migrationBuilder.DropColumn(
                name: "investigatorId",
                table: "Investigation");

            migrationBuilder.DropColumn(
                name: "reportId",
                table: "Investigation");

            migrationBuilder.AddColumn<int>(
                name: "investigatoridNum",
                table: "Investigation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reportidNum",
                table: "Investigation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investigation_investigatoridNum",
                table: "Investigation",
                column: "investigatoridNum");

            migrationBuilder.CreateIndex(
                name: "IX_Investigation_reportidNum",
                table: "Investigation",
                column: "reportidNum");

            migrationBuilder.AddForeignKey(
                name: "FK_Investigation_Investigator_investigatoridNum",
                table: "Investigation",
                column: "investigatoridNum",
                principalTable: "Investigator",
                principalColumn: "idNum",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Investigation_Report_reportidNum",
                table: "Investigation",
                column: "reportidNum",
                principalTable: "Report",
                principalColumn: "idNum",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
