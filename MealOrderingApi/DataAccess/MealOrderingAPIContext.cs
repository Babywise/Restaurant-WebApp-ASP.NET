using Meal_Ordering_Class_Library.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MealOrderingApi.DataAccess
{
    public class MealOrderingAPIContext : IdentityDbContext<User>
    {
        public MealOrderingAPIContext(DbContextOptions options)
            : base(options)
        {
        }

        public static async Task IntitalizeUserIdentities(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Sesame123#";
            string roleName = "Admin";

            string username2 = "issi";
            string password2 = "Issi123$";
            string roleName2 = "Restaurant";

            string username3 = "nick";
            string password3 = "Nick123$";
            string roleName3 = "Customer";

            // Seed custom roles
            var customRoles = new[] { "Admin", "Customer", "Restaurant" };
            foreach (var role in customRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username, FirstName = username, AccountType = roleName};
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username2) == null)
            {
                User user = new User { UserName = username2, FirstName = username2, AccountType = roleName2 };
                var result = await userManager.CreateAsync(user, password2);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName2);
                }
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username3) == null)
            {
                User user = new User { UserName = username3, FirstName = username3, AccountType = roleName3 };
                var result = await userManager.CreateAsync(user, password3);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName3);
                }
            }
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category and Product FK relationship (1-to-Many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product and Category FK relationship (Many-to-1)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderContents and OrderProducts FK relationship (1-to-Many)
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = 1,
                    Name = "Burgers",
                },
                new Category()
                {
                    CategoryId = 2,
                    Name = "Sandwiches",
                },
                new Category()
                {
                    CategoryId = 3,
                    Name = "Salads",
                },
                new Category()
                {
                    CategoryId = 4,
                    Name = "Sides",
                },
                new Category()
                {
                    CategoryId = 5,
                    Name = "Drinks",
                },
                new Category()
                {
                    CategoryId = 6,
                    Name = "Alcoholic Beverages",
                },
                new Category()
                {
                    CategoryId = 7,
                    Name = "Desserts",
                },
                new Category()
                {
                    CategoryId = 8,
                    Name = "Specials",
                }
            );

            modelBuilder.Entity<Product>().HasData(
                // Burgers
                new Product()
                {
                    ProductId = 1,
                    Name = "Classic Burger",
                    Description = "A juicy beef patty served on a freshly baked bun with lettuce, tomato, onion, and pickles.",
                    Quantity = 15,
                    Cost = 8.99f,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Bacon Cheeseburger",
                    Description = "Our classic burger with crispy bacon and melted American cheese on top.",
                    Quantity = 8,
                    Cost = 11.99f,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 3,
                    Name = "Mushroom Swiss Burger",
                    Description = "Our classic burger topped with sautéed mushrooms and melted Swiss cheese.",
                    Quantity = 6,
                    Cost = 11.99f,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 4,
                    Name = "Veggie Burger",
                    Description = "A vegetarian patty made with fresh vegetables and spices, topped with lettuce, tomato, onion, and pickles.",
                    Quantity = 6,
                    Cost = 8.99f,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 5,
                    Name = "BBQ Burger",
                    Description = " classic burger topped with BBQ sauce, crispy onion rings, and cheddar cheese.",
                    Quantity = 8,
                    Cost = 12.49f,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 6,
                    Name = "Double Cheeseburger",
                    Description = "Two juicy beef patties with melted American cheese, lettuce, tomato, onion, and pickles.",
                    Quantity = 10,
                    Cost = 13.99f,
                    StoreId = 1,
                    CategoryId = 1,
                },
                // Sandwiches
                new Product()
                {
                    ProductId = 7,
                    Name = "Grilled Chicken Sandwich",
                    Description = "Grilled chicken breast served on a freshly baked bun with lettuce, tomato, and mayo.",
                    Quantity = 15,
                    Cost = 13.99f,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 8,
                    Name = "BLT",
                    Description = "Crispy bacon, lettuce, tomato, and mayo served on toasted bread.",
                    Quantity = 12,
                    Cost = 7.99f,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 9,
                    Name = "Turkey and Swiss",
                    Description = "Sliced turkey breast and melted Swiss cheese on toasted bread with lettuce, tomato, and mayo. ",
                    Quantity = 10,
                    Cost = 9.99f,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 10,
                    Name = "Philly Cheesesteak",
                    Description = " Sliced steak with sautéed onions and melted provolone cheese on a hoagie roll.",
                    Quantity = 8,
                    Cost = 12.99f,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 11,
                    Name = "Club Sandwich",
                    Description = "Sliced turkey, ham, bacon, lettuce, tomato, and mayo on toasted bread.",
                    Quantity = 6,
                    Cost = 11.49f,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 12,
                    Name = "Triple Grilled Cheese With Tomato Soup",
                    Description = "Melted muenster, mozzarella, and parmesan cheese on toasted bread. Served with tomato soup.",
                    Quantity = 6,
                    Cost = 7.99f,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 13,
                    Name = "Veal or Chicken Parm Sandwich",
                    Description = "Chicken breast or breaded tender veal topped with tomato sauce and mozzarella cheese on a tasted ciabatta bun.",
                    Quantity = 8,
                    Cost = 18.49f,
                    StoreId = 1,
                    CategoryId = 2,
                },
                // Salads
                new Product()
                {
                    ProductId = 14,
                    Name = "Caesar Salad",
                    Description = "Crisp romaine lettuce, croutons, bacon, and shaved parmesan cheese with Caesar dressing.",
                    Quantity = 20,
                    Cost = 7.99f,
                    StoreId = 1,
                    CategoryId = 3,
                },
                new Product()
                {
                    ProductId = 15,
                    Name = "Garden Salad",
                    Description = "Mixed greens, cherry tomatoes, cucumbers, red onion, and carrots with your choice of dressing.",
                    Quantity = 15,
                    Cost = 6.49f,
                    StoreId = 1,
                    CategoryId = 3,
                },
                new Product()
                {
                    ProductId = 16,
                    Name = "Cobb Salad",
                    Description = "Mixed greens, grilled chicken, crispy bacon, avocado, cherry tomatoes, hard-boiled egg, and shredded cheese, topped with chipotle aioli.",
                    Quantity = 8,
                    Cost = 12.99f,
                    StoreId = 1,
                    CategoryId = 3,
                },
                new Product()
                {
                    ProductId = 17,
                    Name = "Greek Salad",
                    Description = "Mixed greens, feta cheese, Kalamata olives, cherry tomatoes, cucumber, and red onion with Greek dressing.",
                    Quantity = 12,
                    Cost = 9.99f,
                    StoreId = 1,
                    CategoryId = 3,
                },
                new Product()
                {
                    ProductId = 18,
                    Name = "Chicken Tender Salad",
                    Description = "Fresh greens topped with battered chicken tenders, hearts of palm, artichokes, bacon, and croutons with honey mustard.",
                    Quantity = 15,
                    Cost = 18.49f,
                    StoreId = 1,
                    CategoryId = 3,
                },
                // Sides
                new Product()
                {
                    ProductId = 19,
                    Name = "French Fries",
                    Description = "Crispy golden fries",
                    Quantity = 50,
                    Cost = 3.99f,
                    StoreId = 1,
                    CategoryId = 4,
                },
                new Product()
                {
                    ProductId = 20,
                    Name = "Onion Rings",
                    Description = "Crispy breaded onion rings.",
                    Quantity = 40,
                    Cost = 4.99f,
                    StoreId = 1,
                    CategoryId = 4,
                },
                new Product()
                {
                    ProductId = 21,
                    Name = "Sweet Potato Fries",
                    Description = "Crispy sweet potato fries.",
                    Quantity = 30,
                    Cost = 4.99f,
                    StoreId = 1,
                    CategoryId = 4,
                },
                // Drinks
                new Product()
                {
                    ProductId = 22,
                    Name = "Fountain Drink",
                    Description = "Unlimited refills and choices of: Coke, Diet Coke, Sprite, Orange Fanta, Lemonade, or Iced Tea.",
                    Quantity = 40,
                    Cost = 3.49f,
                    StoreId = 1,
                    CategoryId = 5,
                },
                new Product()
                {
                    ProductId = 23,
                    Name = "Coffee or Tea",
                    Description = "Unlimited refills",
                    Quantity = 40,
                    Cost = 3.49f,
                    StoreId = 1,
                    CategoryId = 5,
                },
                new Product()
                {
                    ProductId = 24,
                    Name = "Water Bottle",
                    Description = "Nestle",
                    Quantity = 20,
                    Cost = 2.49f,
                    StoreId = 1,
                    CategoryId = 5,
                },
                // Alcoholic Beverages
                new Product()
                {
                    ProductId = 25,
                    Name = "Margarita",
                    Description = "Tequila, triple sec, and lime juice served over ice with a salted rim.",
                    Quantity = 15,
                    Cost = 8.99f,
                    StoreId = 1,
                    CategoryId = 6,
                },
                new Product()
                {
                    ProductId = 26,
                    Name = "Long Island Iced Tea",
                    Description = "Vodka, rum, gin, tequila, triple sec, and lemon juice with a splash of cola served over ice.",
                    Quantity = 12,
                    Cost = 10.99f,
                    StoreId = 1,
                    CategoryId = 6,
                },
                new Product()
                {
                    ProductId = 27,
                    Name = "Mojito",
                    Description = "Rum, lime juice, simple syrup, and fresh mint served over ice.",
                    Quantity = 10,
                    Cost = 9.99f,
                    StoreId = 1,
                    CategoryId = 6,
                },
                new Product()
                {
                    ProductId = 28,
                    Name = "Cosmopolitan",
                    Description = "Vodka, triple sec, lime juice, and cranberry juice served up with a twist of orange.",
                    Quantity = 8,
                    Cost = 11.99f,
                    StoreId = 1,
                    CategoryId = 6,
                },
                new Product()
                {
                    ProductId = 29,
                    Name = "Sangria",
                    Description = "Red or white wine, brandy, fruit juice, and sliced fruit served over ice.",
                    Quantity = 8,
                    Cost = 9.99f,
                    StoreId = 1,
                    CategoryId = 6,
                },
                new Product()
                {
                    ProductId = 30,
                    Name = "Domestic Beer",
                    Description = "Steam Whistle Pilsner, Muskoka Brewery Mad Tom IPA, Beau's Lug Tread Lagered Ale, Amsterdam Brewery Boneshaker IPA, Wellington Brewery SPA (Special Pale Ale)",
                    Quantity = 30,
                    Cost = 6.99f,
                    StoreId = 1,
                    CategoryId = 6,
                },
                new Product()
                {
                    ProductId = 31,
                    Name = "Imported Beer",
                    Description = "Corona Extra (Mexico), Modelo Especial (Mexico), Heineken (Netherlands), Guinness (Ireland), Sapporo (Japan), Stella Artois (Belgium)",
                    Quantity = 30,
                    Cost = 7.99f,
                    StoreId = 1,
                    CategoryId = 6,
                },
                // Desserts
                new Product()
                {
                    ProductId = 32,
                    Name = "Chocolate Lava Cake",
                    Description = "Warm chocolate cake with a molten chocolate center, served with a scoop of vanilla ice cream",
                    Quantity = 10,
                    Cost = 8.99f,
                    StoreId = 1,
                    CategoryId = 7,
                },
                new Product()
                {
                    ProductId = 33,
                    Name = "New York Cheesecake",
                    Description = "Creamy vanilla cheesecake on a graham cracker crust, topped with a fresh berry compote.",
                    Quantity = 8,
                    Cost = 7.99f,
                    StoreId = 1,
                    CategoryId = 7,
                },
                new Product()
                {
                    ProductId = 34,
                    Name = "Apple Pie",
                    Description = "Classic apple pie with a flaky crust, served warm with a scoop of vanilla ice cream.",
                    Quantity = 10,
                    Cost = 6.99f,
                    StoreId = 1,
                    CategoryId = 7,
                },
                new Product()
                {
                    ProductId = 35,
                    Name = "Tiramisu",
                    Description = "Layers of ladyfingers soaked in coffee and rum, with a creamy mascarpone filling and dusted with cocoa powder.",
                    Quantity = 7,
                    Cost = 9.99f,
                    StoreId = 1,
                    CategoryId = 7,
                },
                new Product()
                {
                    ProductId = 36,
                    Name = "Chocolate Brownie Sundae",
                    Description = "Warm chocolate brownie topped with a scoop of vanilla ice cream, whipped cream, and chocolate sauce.",
                    Quantity = 13,
                    Cost = 7.99f,
                    StoreId = 1,
                    CategoryId = 7,
                },
                //Specials
                new Product()
                {
                    ProductId = 37,
                    Name = "Soup of the Day",
                    Description = "Ask your server for details such as a description, price, and availability.",
                    Quantity = 0,
                    Cost = 0.00f,
                    StoreId = 1,
                    CategoryId = 8,
                },
                new Product()
                {
                    ProductId = 38,
                    Name = "Burger of the Day",
                    Description = "Ask your server for details such as a description, price, and availability.",
                    Quantity = 0,
                    Cost = 0.00f,
                    StoreId = 1,
                    CategoryId = 8,
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    OrderId = 1,
                    Username = "nick",
                    StoreId = 1,
                    Status = "In the Oven",
                },
                new Order()
                {
                    OrderId = 2,
                    Username = "nick",
                    StoreId = 1,
                    Status = "Confirmed",
                },
                new Order()
                {
                    OrderId = 3,
                    Username = "nick",
                    StoreId = 1,
                    Status = "Cart",
                }
            );

            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct()
                {
                    OrderProductId = 1,
                    OrderId = 3,
                    ProductId = 1,
                    Quantity = 10,
                },
                new OrderProduct()
                {
                    OrderProductId = 2,
                    OrderId = 3,
                    ProductId = 2,
                    Quantity = 20,
                },
                new OrderProduct()
                {
                    OrderProductId = 3,
                    OrderId = 3,
                    ProductId = 3,
                    Quantity = 14,
                },
                new OrderProduct()
                {
                    OrderProductId = 4,
                    OrderId = 3,
                    ProductId = 4,
                    Quantity = 50,
                },
                new OrderProduct()
                {
                    OrderProductId = 5,
                    OrderId = 3,
                    ProductId = 1,
                    Quantity = 10,
                }
            );

        }
    }
}
 