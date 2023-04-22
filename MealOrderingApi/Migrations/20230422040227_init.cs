using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealOrderingApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.OrderProductId);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Burgers" },
                    { 2, false, "Sandwiches" },
                    { 3, false, "Salads" },
                    { 4, false, "Sides" },
                    { 5, false, "Drinks" },
                    { 6, false, "Alcoholic Beverages" },
                    { 7, false, "Desserts" },
                    { 8, false, "Specials" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Status", "StoreId", "Username" },
                values: new object[,]
                {
                    { 1, "Cooking", 1, "issi" },
                    { 2, "Pending", 1, "nick" },
                    { 3, "Cart", 1, "issi" },
                    { 4, "Delivered", 1, "nick" },
                    { 5, "ODelivery", 1, "issi" },
                    { 6, "Preparation", 1, "nick" },
                    { 7, "Confirmed", 1, "issi" },
                    { 8, "QC", 1, "nick" },
                    { 9, "Cooking", 1, "nick" },
                    { 10, "Delivered", 1, "issi" },
                    { 11, "Preparation", 1, "issi" },
                    { 12, "Preparation", 1, "issi" },
                    { 13, "Cooking", 1, "nick" },
                    { 14, "ODelivery", 1, "nick" }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderProductId", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 5, 3 },
                    { 3, 1, 9, 1 },
                    { 4, 2, 13, 1 },
                    { 5, 2, 18, 2 },
                    { 6, 2, 22, 1 },
                    { 7, 3, 27, 4 },
                    { 8, 3, 31, 1 },
                    { 9, 3, 35, 1 },
                    { 10, 4, 2, 1 },
                    { 11, 4, 7, 1 },
                    { 12, 4, 11, 1 },
                    { 13, 5, 15, 2 },
                    { 14, 5, 20, 1 },
                    { 15, 5, 24, 2 },
                    { 16, 6, 29, 1 },
                    { 17, 6, 33, 1 },
                    { 18, 7, 37, 10 },
                    { 19, 8, 3, 3 },
                    { 20, 8, 6, 1 },
                    { 21, 8, 10, 1 },
                    { 22, 8, 14, 2 },
                    { 23, 8, 19, 1 },
                    { 24, 8, 23, 1 },
                    { 25, 9, 28, 1 },
                    { 26, 9, 32, 2 },
                    { 27, 10, 36, 1 },
                    { 28, 10, 4, 2 },
                    { 29, 10, 8, 1 },
                    { 30, 10, 12, 3 },
                    { 31, 10, 17, 1 },
                    { 32, 11, 21, 1 },
                    { 33, 12, 16, 3 },
                    { 34, 13, 30, 3 },
                    { 35, 13, 34, 1 },
                    { 36, 13, 38, 1 },
                    { 37, 14, 16, 1 },
                    { 38, 14, 39, 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Cost", "Description", "IsDeleted", "Name", "Quantity", "StoreId" },
                values: new object[,]
                {
                    { 1, 1, 8.99f, "A juicy beef patty served on a freshly baked bun with lettuce, tomato, onion, and pickles.", false, "Classic Burger", 15, 1 },
                    { 2, 1, 11.99f, "Our classic burger with crispy bacon and melted American cheese on top.", false, "Bacon Cheeseburger", 8, 1 },
                    { 3, 1, 11.99f, "Our classic burger topped with sautéed mushrooms and melted Swiss cheese.", false, "Mushroom Swiss Burger", 6, 1 },
                    { 4, 1, 8.99f, "A vegetarian patty made with fresh vegetables and spices, topped with lettuce, tomato, onion, and pickles.", false, "Veggie Burger", 6, 1 },
                    { 5, 1, 12.49f, " classic burger topped with BBQ sauce, crispy onion rings, and cheddar cheese.", false, "BBQ Burger", 8, 1 },
                    { 6, 1, 13.99f, "Two juicy beef patties with melted American cheese, lettuce, tomato, onion, and pickles.", false, "Double Cheeseburger", 10, 1 },
                    { 7, 2, 13.99f, "Grilled chicken breast served on a freshly baked bun with lettuce, tomato, and mayo.", false, "Grilled Chicken Sandwich", 15, 1 },
                    { 8, 2, 7.99f, "Crispy bacon, lettuce, tomato, and mayo served on toasted bread.", false, "BLT", 12, 1 },
                    { 9, 2, 9.99f, "Sliced turkey breast and melted Swiss cheese on toasted bread with lettuce, tomato, and mayo. ", false, "Turkey and Swiss", 10, 1 },
                    { 10, 2, 12.99f, " Sliced steak with sautéed onions and melted provolone cheese on a hoagie roll.", false, "Philly Cheesesteak", 8, 1 },
                    { 11, 2, 11.49f, "Sliced turkey, ham, bacon, lettuce, tomato, and mayo on toasted bread.", false, "Club Sandwich", 6, 1 },
                    { 12, 2, 7.99f, "Melted muenster, mozzarella, and parmesan cheese on toasted bread. Served with tomato soup.", false, "Triple Grilled Cheese With Tomato Soup", 6, 1 },
                    { 13, 2, 18.49f, "Chicken breast or breaded tender veal topped with tomato sauce and mozzarella cheese on a tasted ciabatta bun.", false, "Veal or Chicken Parm Sandwich", 8, 1 },
                    { 14, 3, 7.99f, "Crisp romaine lettuce, croutons, bacon, and shaved parmesan cheese with Caesar dressing.", false, "Caesar Salad", 20, 1 },
                    { 15, 3, 6.49f, "Mixed greens, cherry tomatoes, cucumbers, red onion, and carrots with your choice of dressing.", false, "Garden Salad", 15, 1 },
                    { 16, 3, 12.99f, "Mixed greens, grilled chicken, crispy bacon, avocado, cherry tomatoes, hard-boiled egg, and shredded cheese, topped with chipotle aioli.", false, "Cobb Salad", 8, 1 },
                    { 17, 3, 9.99f, "Mixed greens, feta cheese, Kalamata olives, cherry tomatoes, cucumber, and red onion with Greek dressing.", false, "Greek Salad", 12, 1 },
                    { 18, 3, 18.49f, "Fresh greens topped with battered chicken tenders, hearts of palm, artichokes, bacon, and croutons with honey mustard.", false, "Chicken Tender Salad", 15, 1 },
                    { 19, 4, 3.99f, "Crispy golden fries", false, "French Fries", 50, 1 },
                    { 20, 4, 4.99f, "Crispy breaded onion rings.", false, "Onion Rings", 40, 1 },
                    { 21, 4, 4.99f, "Crispy sweet potato fries.", false, "Sweet Potato Fries", 30, 1 },
                    { 22, 5, 3.49f, "Unlimited refills and choices of: Coke, Diet Coke, Sprite, Orange Fanta, Lemonade, or Iced Tea.", false, "Fountain Drink", 40, 1 },
                    { 23, 5, 3.49f, "Unlimited refills", false, "Coffee or Tea", 40, 1 },
                    { 24, 5, 2.49f, "Nestle", false, "Water Bottle", 20, 1 },
                    { 25, 6, 8.99f, "Tequila, triple sec, and lime juice served over ice with a salted rim.", false, "Margarita", 15, 1 },
                    { 26, 6, 10.99f, "Vodka, rum, gin, tequila, triple sec, and lemon juice with a splash of cola served over ice.", false, "Long Island Iced Tea", 12, 1 },
                    { 27, 6, 9.99f, "Rum, lime juice, simple syrup, and fresh mint served over ice.", false, "Mojito", 10, 1 },
                    { 28, 6, 11.99f, "Vodka, triple sec, lime juice, and cranberry juice served up with a twist of orange.", false, "Cosmopolitan", 8, 1 },
                    { 29, 6, 9.99f, "Red or white wine, brandy, fruit juice, and sliced fruit served over ice.", false, "Sangria", 8, 1 },
                    { 30, 6, 6.99f, "Steam Whistle Pilsner, Muskoka Brewery Mad Tom IPA, Beau's Lug Tread Lagered Ale, Amsterdam Brewery Boneshaker IPA, Wellington Brewery SPA (Special Pale Ale)", false, "Domestic Beer", 30, 1 },
                    { 31, 6, 7.99f, "Corona Extra (Mexico), Modelo Especial (Mexico), Heineken (Netherlands), Guinness (Ireland), Sapporo (Japan), Stella Artois (Belgium)", false, "Imported Beer", 30, 1 },
                    { 32, 7, 8.99f, "Warm chocolate cake with a molten chocolate center, served with a scoop of vanilla ice cream", false, "Chocolate Lava Cake", 10, 1 },
                    { 33, 7, 7.99f, "Creamy vanilla cheesecake on a graham cracker crust, topped with a fresh berry compote.", false, "New York Cheesecake", 8, 1 },
                    { 34, 7, 6.99f, "Classic apple pie with a flaky crust, served warm with a scoop of vanilla ice cream.", false, "Apple Pie", 10, 1 },
                    { 35, 7, 9.99f, "Layers of ladyfingers soaked in coffee and rum, with a creamy mascarpone filling and dusted with cocoa powder.", false, "Tiramisu", 7, 1 },
                    { 36, 7, 7.99f, "Warm chocolate brownie topped with a scoop of vanilla ice cream, whipped cream, and chocolate sauce.", false, "Chocolate Brownie Sundae", 13, 1 },
                    { 37, 8, 0f, "Ask your server for details such as a description, price, and availability.", false, "Soup of the Day", 0, 1 },
                    { 38, 8, 0f, "Ask your server for details such as a description, price, and availability.", false, "Burger of the Day", 0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
