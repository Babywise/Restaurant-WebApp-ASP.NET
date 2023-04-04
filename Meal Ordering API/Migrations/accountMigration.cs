using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meal_Ordering_API.Migrations
{
    /// <inheritdoc />
    public partial class accountMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ApiKey = table.Column<Guid>(type: "varchar(45)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(45)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(45)", nullable: true),
                    Username = table.Column<string>(type: "varchar(45)", nullable: false),
                    Password = table.Column<string>(type: "varchar(45)", nullable: false),
                    AccountType = table.Column<string>(type: "varchar(45)", nullable: false),
                    Email = table.Column<string>(type: "varchar(45)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(45)", nullable: true),
                    Address = table.Column<string>(type: "varchar(45)", nullable: false)

                },
                constraints: table => {
                    table.PrimaryKey("id", x => x.Id);
                    
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
