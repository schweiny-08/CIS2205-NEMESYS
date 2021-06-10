using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemesys.Migrations
{
    public partial class extended_identityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "faculty",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "faculty",
                table: "AspNetUsers");
        }
    }
}
