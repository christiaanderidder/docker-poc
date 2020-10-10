using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 294, DateTimeKind.Unspecified).AddTicks(302), new TimeSpan(0, 2, 0, 0, 0)), null, "First product", "Product 1", 10m, new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4603), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4927), new TimeSpan(0, 2, 0, 0, 0)), null, "Second product", "Product 2", 15m, new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4935), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[] { 3, new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4938), new TimeSpan(0, 2, 0, 0, 0)), null, "Third product", "Product 3", 20m, new DateTimeOffset(new DateTime(2020, 10, 1, 13, 45, 12, 295, DateTimeKind.Unspecified).AddTicks(4941), new TimeSpan(0, 2, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
