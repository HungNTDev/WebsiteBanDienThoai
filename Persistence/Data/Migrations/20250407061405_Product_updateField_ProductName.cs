using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Product_updateField_ProductName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductItemName",
                table: "Inventories");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductItems");

            migrationBuilder.AddColumn<string>(
                name: "ProductItemName",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
