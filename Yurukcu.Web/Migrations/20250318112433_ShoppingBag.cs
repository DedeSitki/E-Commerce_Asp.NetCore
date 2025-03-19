using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yurukcu.Web.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingBag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingBags",
                columns: table => new
                {
                    ShoppingBagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropductId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBags", x => x.ShoppingBagId);
                    table.ForeignKey(
                        name: "FK_ShoppingBags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_ShoppingBags_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_ProductId",
                table: "ShoppingBags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_UserId1",
                table: "ShoppingBags",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingBags");
        }
    }
}
