using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yurukcu.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingBagsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Users_UserId1",
                table: "ShoppingBags");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBags_UserId1",
                table: "ShoppingBags");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ShoppingBags");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ShoppingBags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_UserId",
                table: "ShoppingBags",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Users_UserId",
                table: "ShoppingBags",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Users_UserId",
                table: "ShoppingBags");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBags_UserId",
                table: "ShoppingBags");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingBags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "ShoppingBags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_UserId1",
                table: "ShoppingBags",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Users_UserId1",
                table: "ShoppingBags",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
