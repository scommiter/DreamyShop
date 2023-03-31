using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamyShop.EntityFrameworkCore.Migrations
{
    public partial class UpdateProductName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Label",
                table: "ProductAttributes",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductAttributes",
                newName: "Label");
        }
    }
}
