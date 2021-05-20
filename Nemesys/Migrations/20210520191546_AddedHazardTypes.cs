using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemesys.Migrations
{
    public partial class AddedHazardTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hazardId",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "hazardTypeId",
                table: "Report",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HazardType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hazardTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_hazardTypeId",
                table: "Report",
                column: "hazardTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_HazardType_hazardTypeId",
                table: "Report",
                column: "hazardTypeId",
                principalTable: "HazardType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_HazardType_hazardTypeId",
                table: "Report");

            migrationBuilder.DropTable(
                name: "HazardType");

            migrationBuilder.DropIndex(
                name: "IX_Report_hazardTypeId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "hazardId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "hazardTypeId",
                table: "Report");
        }
    }
}
