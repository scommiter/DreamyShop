using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamyShop.EntityFrameworkCore.Migrations
{
    public partial class AddBillCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillCode",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillCode",
                table: "Bills");
        }
    }
}
