using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealOrderingApi.Migrations
{
    /// <inheritdoc />
    public partial class updatedrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_OrderContents_OrderContentsId",
                table: "OrderProduct");

            migrationBuilder.DropTable(
                name: "OrderContents");

            migrationBuilder.RenameColumn(
                name: "OrderContentsId",
                table: "OrderProduct",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_OrderContentsId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_OrderId");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "Status",
                value: "Confirmed");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "Status", "StoreId" },
                values: new object[] { 3, 1, "Canceled", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct");

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderProduct",
                newName: "OrderContentsId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_OrderId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_OrderContentsId");

            migrationBuilder.CreateTable(
                name: "OrderContents",
                columns: table => new
                {
                    OrderContentsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderContents", x => x.OrderContentsId);
                    table.ForeignKey(
                        name: "FK_OrderContents_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderContents",
                columns: new[] { "OrderContentsId", "OrderId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "Status",
                value: "In the Oven");

            migrationBuilder.CreateIndex(
                name: "IX_OrderContents_OrderId",
                table: "OrderContents",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_OrderContents_OrderContentsId",
                table: "OrderProduct",
                column: "OrderContentsId",
                principalTable: "OrderContents",
                principalColumn: "OrderContentsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
