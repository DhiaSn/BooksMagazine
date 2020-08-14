using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksMagazine.Migrations
{
    public partial class update_team_about_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_About_AboutId",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Worker_AboutId",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "AboutId",
                table: "Worker");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Worker",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "About",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Worker_TeamId",
                table: "Worker",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_About_TeamId",
                table: "About",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_About_Team_TeamId",
                table: "About",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Team_TeamId",
                table: "Worker",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_About_Team_TeamId",
                table: "About");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Team_TeamId",
                table: "Worker");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Worker_TeamId",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_About_TeamId",
                table: "About");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "About");

            migrationBuilder.AddColumn<int>(
                name: "AboutId",
                table: "Worker",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_AboutId",
                table: "Worker",
                column: "AboutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_About_AboutId",
                table: "Worker",
                column: "AboutId",
                principalTable: "About",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
