using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealOrderingApi.Migrations
{
    /// <inheritdoc />
    public partial class more : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderProductId", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 6, 1, 1, 10 },
                    { 7, 2, 2, 20 },
                    { 8, 4, 3, 14 },
                    { 9, 5, 4, 50 },
                    { 10, 6, 1, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "Status",
                value: "Cooking");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "Status",
                value: "ODelivery");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "Status",
                value: "Preparation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "OrderProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "OrderProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "OrderProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "OrderProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "OrderProductId",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "Status",
                value: "In the oven");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "Status",
                value: "Out for delivery");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "Status",
                value: "Order is being prepared");
        }
    }
}
