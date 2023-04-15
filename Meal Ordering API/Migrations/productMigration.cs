using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meal_Ordering_API.Migrations
{
    /// <inheritdoc />
    public partial class productMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "Product",
             columns: table => new
             {
                 Id = table.Column<int>(type: "int", nullable: false),
                 Cost = table.Column<float>(type: "float", nullable: true),
                 Inventory = table.Column<int>(type: "int", nullable: true),
                 Name = table.Column<string>(type: "varchar(45)", nullable: true),
                 CategoryId = table.Column<int>(type: "int", nullable: true),
                 StoreId = table.Column<int>(type: "int", nullable: true),
                 Quantity = table.Column<int>(type: "int", nullable: true),
                 Available = table.Column<bool>(type: "tinyint", nullable: true),
                 orderId = table.Column<int>(type: "int", nullable: true),
                 status = table.Column<bool>(type: "tinyint", nullable: true),
                 customerId = table.Column<int>(type: "int", nullable: true),
             },
             constraints: table => {
                 table.PrimaryKey("productId", x => x.Id);
                 table.ForeignKey("categoryId", x => x.CategoryId,"Category");
                 table.ForeignKey("accountId", x => x.StoreId, "Account");
                 table.ForeignKey("productCustomerId", x => x.customerId, "Account");
             }
             );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
