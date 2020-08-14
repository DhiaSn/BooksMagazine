using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksMagazine.Migrations
{
    public partial class update_Mession_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Messions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Messions");
        }
    }
}
