using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yurukcu.Web.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingBag2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Products_ProductId",
                table: "ShoppingBags");

            migrationBuilder.DropColumn(
                name: "PropductId",
                table: "ShoppingBags");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShoppingBags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Products_ProductId",
                table: "ShoppingBags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Products_ProductId",
                table: "ShoppingBags");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShoppingBags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PropductId",
                table: "ShoppingBags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Products_ProductId",
                table: "ShoppingBags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
