using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealOrderingApi.Migrations
{
    /// <inheritdoc />
    public partial class updateentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Orders",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "IsUpdated",
                value: null);
        }
    }
}
