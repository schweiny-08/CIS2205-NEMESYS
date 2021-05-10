using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemesys.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reporter",
                columns: table => new
                {
                    idNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporter", x => x.idNum);
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
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    upvotes = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investigator",
                columns: table => new
                {
                    idNum = table.Column<int>(type: "int", nullable: false),
                    deptNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigator", x => x.idNum);
                    table.ForeignKey(
                        name: "FK_Investigator_Reporter_idNum",
                        column: x => x.idNum,
                        principalTable: "Reporter",
                        principalColumn: "idNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    idNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    upvotes = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    reporteridNum = table.Column<int>(type: "int", nullable: true),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.idNum);
                    table.ForeignKey(
                        name: "FK_Report_Reporter_reporteridNum",
                        column: x => x.reporteridNum,
                        principalTable: "Reporter",
                        principalColumn: "idNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Investigation",
                columns: table => new
                {
                    idNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    investigatoridNum = table.Column<int>(type: "int", nullable: true),
                    reportidNum = table.Column<int>(type: "int", nullable: true),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigation", x => x.idNum);
                    table.ForeignKey(
                        name: "FK_Investigation_Investigator_investigatoridNum",
                        column: x => x.investigatoridNum,
                        principalTable: "Investigator",
                        principalColumn: "idNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Investigation_Report_reportidNum",
                        column: x => x.reportidNum,
                        principalTable: "Report",
                        principalColumn: "idNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investigation_investigatoridNum",
                table: "Investigation",
                column: "investigatoridNum");

            migrationBuilder.CreateIndex(
                name: "IX_Investigation_reportidNum",
                table: "Investigation",
                column: "reportidNum");

            migrationBuilder.CreateIndex(
                name: "IX_Report_reporteridNum",
                table: "Report",
                column: "reporteridNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investigation");

            migrationBuilder.DropTable(
                name: "ReportViewModel");

            migrationBuilder.DropTable(
                name: "Investigator");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Reporter");
        }
    }
}
