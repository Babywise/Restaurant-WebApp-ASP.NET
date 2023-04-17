using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealOrderingApi.Migrations
{
    /// <inheritdoc />
    public partial class MOARPIZZA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "Description",
                value: "SoOoOoo Many mushrooms!");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "Name", "Quantity" },
                values: new object[] { 1, "Not for Vegans!", "MeatLovers Pizza", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "Name", "Quantity" },
                values: new object[] { 1, "For Vegans!", "Veggie Pizza", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "CategoryId", "Cost", "Description", "Name", "Quantity" },
                values: new object[] { 1, 10f, "Not Quite Like the Drink!", "Margherita Pizza", 50 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Cost", "Description", "IsDeleted", "Name", "Quantity", "StoreId" },
                values: new object[,]
                {
                    { 7, 1, 5f, "Chicken Slathered on toppa da pie! Sweet and Tangy Sauce!", false, "BBQ Chicken Pizza", 100, 1 },
                    { 8, 1, 5f, "For a spicy kick!", false, "Buffalo Pizza", 50, 1 },
                    { 9, 1, 10f, "This thing has it all!", false, "The Works Pizza", 50, 1 },
                    { 10, 1, 5f, "For our hot pocket lovers!", false, "Pepperoni Panzarotti", 100, 1 },
                    { 11, 1, 5f, "Imagine a square focaccia pizza!", false, "Roman-style Pizza", 50, 1 },
                    { 12, 2, 5f, "Sweet & Delicious!", false, "Honey Garlic Wings", 500, 1 },
                    { 13, 2, 5f, "What a combination!", false, "Sweet & Spicy Wings", 500, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "Description",
                value: "SoOoOoo many mushrooms!");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "Name", "Quantity" },
                values: new object[] { 2, "Spicy & Delicious!", "Buffalo Wings", 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "Name", "Quantity" },
                values: new object[] { 2, "Sweet & Delicious!", "Honey Garlic Wings", 500 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "CategoryId", "Cost", "Description", "Name", "Quantity" },
                values: new object[] { 2, 5f, "What a combination!", "Sweet & Spicy Wings", 500 });
        }
    }
}
