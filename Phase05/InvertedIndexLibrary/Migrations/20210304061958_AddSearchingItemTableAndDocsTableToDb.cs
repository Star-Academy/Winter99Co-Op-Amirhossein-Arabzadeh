using Microsoft.EntityFrameworkCore.Migrations;

namespace InvertedIndexLibrary.Migrations
{
    public partial class AddSearchingItemTableAndDocsTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Docs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchingItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchingItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocSearchItem",
                columns: table => new
                {
                    DocsId = table.Column<int>(type: "int", nullable: false),
                    SearchItemsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocSearchItem", x => new { x.DocsId, x.SearchItemsId });
                    table.ForeignKey(
                        name: "FK_DocSearchItem_Docs_DocsId",
                        column: x => x.DocsId,
                        principalTable: "Docs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocSearchItem_SearchingItems_SearchItemsId",
                        column: x => x.SearchItemsId,
                        principalTable: "SearchingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocSearchItem_SearchItemsId",
                table: "DocSearchItem",
                column: "SearchItemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocSearchItem");

            migrationBuilder.DropTable(
                name: "Docs");

            migrationBuilder.DropTable(
                name: "SearchingItems");
        }
    }
}
