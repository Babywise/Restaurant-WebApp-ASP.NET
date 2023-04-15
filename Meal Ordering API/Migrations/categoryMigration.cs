using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meal_Ordering_API.Migrations
{
    /// <inheritdoc />
    public partial class categoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "Category",
             columns: table => new
             {
                 Id = table.Column<int>(type: "int", nullable: false),
                 Name = table.Column<string>(type: "varchar(45)", nullable: true),

             },
             constraints: table => {
                 table.PrimaryKey("myId", x => x.Id);
             }
             );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
