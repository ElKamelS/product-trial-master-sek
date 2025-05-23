using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJECT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "inventoryStatus",
                table: "Products",
                newName: "InventoryStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InventoryStatus",
                table: "Products",
                newName: "inventoryStatus");
        }
    }
}
