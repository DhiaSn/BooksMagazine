using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksMagazine.Migrations
{
    public partial class update_Mession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Messions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Messions");
        }
    }
}
