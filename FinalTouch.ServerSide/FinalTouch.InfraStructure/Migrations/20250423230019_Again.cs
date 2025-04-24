using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalTouch.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77ffff82-87f1-45d5-aa55-2154ad0c9856");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1ab6fef-bfee-4b33-a715-4becc481ef7d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a507bc87-ae8d-4e73-9298-d00504ce0914", null, "Admin", "ADMIN" },
                    { "bbf9df2c-8099-4db7-811a-4be177522bc9", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a507bc87-ae8d-4e73-9298-d00504ce0914");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbf9df2c-8099-4db7-811a-4be177522bc9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77ffff82-87f1-45d5-aa55-2154ad0c9856", null, "Admin", "ADMIN" },
                    { "c1ab6fef-bfee-4b33-a715-4becc481ef7d", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
