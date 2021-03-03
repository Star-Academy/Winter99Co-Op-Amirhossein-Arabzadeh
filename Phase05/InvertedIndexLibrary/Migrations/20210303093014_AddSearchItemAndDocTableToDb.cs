using Microsoft.EntityFrameworkCore.Migrations;

namespace InvertedIndexLibrary.Migrations
{
    public partial class AddSearchItemAndDocTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Docs",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SearchItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Docs_SearchItems_SearchItemId",
                        column: x => x.SearchItemId,
                        principalTable: "SearchItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Docs_SearchItemId",
                table: "Docs",
                column: "SearchItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Docs");

            migrationBuilder.DropTable(
                name: "SearchItems");
        }
    }
}
