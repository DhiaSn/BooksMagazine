using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksMagazine.Migrations
{
    public partial class aboutMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessionsId",
                table: "Purpose",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Paragraph = table.Column<string>(nullable: true),
                    AboutUsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Description_AboutUs_AboutUsId",
                        column: x => x.AboutUsId,
                        principalTable: "AboutUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutUsId = table.Column<int>(nullable: true),
                    MessionsId = table.Column<int>(nullable: true),
                    VisionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.Id);
                    table.ForeignKey(
                        name: "FK_About_AboutUs_AboutUsId",
                        column: x => x.AboutUsId,
                        principalTable: "AboutUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_About_Messions_MessionsId",
                        column: x => x.MessionsId,
                        principalTable: "Messions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_About_Messions_VisionId",
                        column: x => x.VisionId,
                        principalTable: "Messions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    Profession = table.Column<string>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true),
                    FacebookLink = table.Column<string>(nullable: true),
                    GoogleLink = table.Column<string>(nullable: true),
                    TwitterLink = table.Column<string>(nullable: true),
                    LinkedInLink = table.Column<string>(nullable: true),
                    AboutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worker_About_AboutId",
                        column: x => x.AboutId,
                        principalTable: "About",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purpose_MessionsId",
                table: "Purpose",
                column: "MessionsId");

            migrationBuilder.CreateIndex(
                name: "IX_About_AboutUsId",
                table: "About",
                column: "AboutUsId");

            migrationBuilder.CreateIndex(
                name: "IX_About_MessionsId",
                table: "About",
                column: "MessionsId");

            migrationBuilder.CreateIndex(
                name: "IX_About_VisionId",
                table: "About",
                column: "VisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Description_AboutUsId",
                table: "Description",
                column: "AboutUsId");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_AboutId",
                table: "Worker",
                column: "AboutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purpose_Messions_MessionsId",
                table: "Purpose",
                column: "MessionsId",
                principalTable: "Messions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purpose_Messions_MessionsId",
                table: "Purpose");

            migrationBuilder.DropTable(
                name: "Description");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "Messions");

            migrationBuilder.DropIndex(
                name: "IX_Purpose_MessionsId",
                table: "Purpose");

            migrationBuilder.DropColumn(
                name: "MessionsId",
                table: "Purpose");
        }
    }
}
