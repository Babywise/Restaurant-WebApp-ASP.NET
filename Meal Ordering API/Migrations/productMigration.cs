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
             name: "product",
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
             },
             constraints: table => {
                 table.PrimaryKey("productId", x => x.Id);
                 table.ForeignKey("categoryId", x => x.CategoryId,"category");
                 table.ForeignKey("accountId", x => x.StoreId, "account");
             }
             );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
