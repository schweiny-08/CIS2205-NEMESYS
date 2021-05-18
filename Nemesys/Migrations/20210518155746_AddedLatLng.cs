using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemesys.Migrations
{
    public partial class AddedLatLng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "location",
                table: "Report",
                newName: "longitude");

            migrationBuilder.AddColumn<string>(
                name: "latitude",
                table: "Report",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "Report");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Report",
                newName: "location");
        }
    }
}
