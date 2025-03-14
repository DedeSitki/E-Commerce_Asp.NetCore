using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yurukcu.Web.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "DiscountProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountAmount",
                table: "DiscountProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
