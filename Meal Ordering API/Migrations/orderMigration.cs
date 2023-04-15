using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meal_Ordering_API.Migrations
{
    /// <inheritdoc />
    public partial class orderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
     name: "Order",
     columns: table => new
     {
         Id = table.Column<int>(type: "int", nullable: false),
         CustomerId = table.Column<int>(type: "int", nullable: false),
         StoreId = table.Column<int>(type: "int", nullable: false),
         Updated = table.Column<bool>(type: "tinyint", nullable: true),
         Status = table.Column<string>(type: "varchar(45)", nullable: true),



     },
     constraints: table => {
         table.PrimaryKey("orderId", x => x.Id);
         table.ForeignKey("customerId", x => x.CustomerId, "Account");
         table.ForeignKey("storeId", x => x.StoreId, "Account");
     }
     );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
