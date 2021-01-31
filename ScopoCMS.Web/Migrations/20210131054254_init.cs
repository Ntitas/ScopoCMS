using Microsoft.EntityFrameworkCore.Migrations;

namespace ScopoCMS.Web.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_sections_sectionId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_sectionId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "sectionId",
                table: "posts");

            migrationBuilder.CreateTable(
                name: "PostSection",
                columns: table => new
                {
                    PostspostID = table.Column<int>(type: "int", nullable: false),
                    SectionssectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSection", x => new { x.PostspostID, x.SectionssectionId });
                    table.ForeignKey(
                        name: "FK_PostSection_posts_PostspostID",
                        column: x => x.PostspostID,
                        principalTable: "posts",
                        principalColumn: "postID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostSection_sections_SectionssectionId",
                        column: x => x.SectionssectionId,
                        principalTable: "sections",
                        principalColumn: "sectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostSection_SectionssectionId",
                table: "PostSection",
                column: "SectionssectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostSection");

            migrationBuilder.AddColumn<int>(
                name: "sectionId",
                table: "posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_posts_sectionId",
                table: "posts",
                column: "sectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_sections_sectionId",
                table: "posts",
                column: "sectionId",
                principalTable: "sections",
                principalColumn: "sectionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
