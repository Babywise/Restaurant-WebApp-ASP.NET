using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealOrderingApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrdertoCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "Status",
                value: "Cart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "Status",
                value: "Canceled");
        }
    }
}
