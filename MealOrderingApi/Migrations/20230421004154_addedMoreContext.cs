using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealOrderingApi.Migrations
{
    /// <inheritdoc />
    public partial class addedMoreContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Status", "StoreId", "Username" },
                values: new object[,]
                {
                    { 7, "Confirmed", 1, "issi" },
                    { 8, "QC", 1, "issi" }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderProductId", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 11, 7, 4, 50 },
                    { 12, 7, 1, 10 },
                    { 13, 8, 1, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "OrderProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "OrderProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "OrderProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8);
        }
    }
}
