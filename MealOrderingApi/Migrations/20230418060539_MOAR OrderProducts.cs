using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealOrderingApi.Migrations
{
    /// <inheritdoc />
    public partial class MOAROrderProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderProduct",
                keyColumn: "OrderProductId",
                keyValue: 1,
                column: "OrderId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderProduct",
                keyColumn: "OrderProductId",
                keyValue: 2,
                column: "OrderId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderProduct",
                keyColumn: "OrderProductId",
                keyValue: 3,
                column: "OrderId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderProduct",
                keyColumn: "OrderProductId",
                keyValue: 4,
                column: "OrderId",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderProduct",
                keyColumn: "OrderProductId",
                keyValue: 1,
                column: "OrderId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "OrderProduct",
                keyColumn: "OrderProductId",
                keyValue: 2,
                column: "OrderId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "OrderProduct",
                keyColumn: "OrderProductId",
                keyValue: 3,
                column: "OrderId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderProduct",
                keyColumn: "OrderProductId",
                keyValue: 4,
                column: "OrderId",
                value: 2);
        }
    }
}
