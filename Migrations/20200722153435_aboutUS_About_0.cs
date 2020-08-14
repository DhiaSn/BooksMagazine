using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksMagazine.Migrations
{
    public partial class aboutUS_About_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "AboutUs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "AboutUs");
        }
    }
}
