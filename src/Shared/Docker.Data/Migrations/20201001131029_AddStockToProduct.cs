using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Data.Migrations
{
    public partial class AddStockToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 10, 1, 15, 10, 28, 969, DateTimeKind.Unspecified).AddTicks(8260), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 10, 1, 15, 10, 28, 971, DateTimeKind.Unspecified).AddTicks(3197), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 10, 1, 15, 10, 28, 971, DateTimeKind.Unspecified).AddTicks(3519), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 10, 1, 15, 10, 28, 971, DateTimeKind.Unspecified).AddTicks(3529), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 10, 1, 15, 10, 28, 971, DateTimeKind.Unspecified).AddTicks(3532), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 10, 1, 15, 10, 28, 971, DateTimeKind.Unspecified).AddTicks(3536), new TimeSpan(0, 2, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 294, DateTimeKind.Unspecified).AddTicks(302), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4603), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4927), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4935), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4938), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4941), new TimeSpan(0, 2, 0, 0, 0)) });
        }
    }
}
