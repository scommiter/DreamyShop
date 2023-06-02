using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamyShop.EntityFrameworkCore.Migrations
{
    public partial class UpdateNullProductVariant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(3992), new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4008) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4012), new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4013) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4015), new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4016) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4018), new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4019) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4027), new DateTime(2023, 6, 2, 9, 0, 1, 374, DateTimeKind.Local).AddTicks(4028) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2114), new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2128) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2131), new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2132) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2133), new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2134) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2135), new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2139) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2154), new DateTime(2023, 5, 23, 0, 28, 35, 329, DateTimeKind.Local).AddTicks(2155) });
        }
    }
}
