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

            string username2 = "restaurant";
            string password2 = "Sesame123#";
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
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username2) == null)
            {
                User user = new User { UserName = username2 };
                var result = await userManager.CreateAsync(user, password2);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName2);
                }
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username3) == null)
            {
                User user = new User { UserName = username3 };
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
                    Name = "Pizza",
                },
                new Category()
                {
                    CategoryId = 2,
                    Name = "Wings",
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    Name = "Cheese Pizza",
                    Description = "Delicious & Cheesy!",
                    Quantity = 100,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Pepperoni Pizza",
                    Description = "Delicious & Cheesy!",
                    Quantity = 50,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 3,
                    Name = "Canadian Pizza",
                    Description = "SoOoOoo Many mushrooms!",
                    Quantity = 50,
                    Cost = 10,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 4,
                    Name = "MeatLovers Pizza",
                    Description = "Not for Vegans!",
                    Quantity = 100,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 5,
                    Name = "Veggie Pizza",
                    Description = "For Vegans!",
                    Quantity = 50,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 6,
                    Name = "Margherita Pizza",
                    Description = "Not Quite Like the Drink!",
                    Quantity = 50,
                    Cost = 10,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 7,
                    Name = "BBQ Chicken Pizza",
                    Description = "Chicken Slathered on toppa da pie! Sweet and Tangy Sauce!",
                    Quantity = 100,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 8,
                    Name = "Buffalo Pizza",
                    Description = "For a spicy kick!",
                    Quantity = 50,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 9,
                    Name = "The Works Pizza",
                    Description = "This thing has it all!",
                    Quantity = 50,
                    Cost = 10,
                    StoreId = 1,
                    CategoryId = 1,
                }, new Product()
                {
                    ProductId = 10,
                    Name = "Pepperoni Panzarotti",
                    Description = "For our hot pocket lovers!",
                    Quantity = 100,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 11,
                    Name = "Roman-style Pizza",
                    Description = "Imagine a square focaccia pizza!",
                    Quantity = 50,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 12,
                    Name = "Honey Garlic Wings",
                    Description = "Sweet & Delicious!",
                    Quantity = 500,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 13,
                    Name = "Sweet & Spicy Wings",
                    Description = "What a combination!",
                    Quantity = 500,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 2,
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
 