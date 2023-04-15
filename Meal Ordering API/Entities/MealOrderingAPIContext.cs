using Meal_Ordering_Class_Library.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Meal_Ordering_WebApp.Entities
{
    public class MealOrderingAPIContext : IdentityDbContext<Account>
    {
        public MealOrderingAPIContext(DbContextOptions<MealOrderingAPIContext> options)
            : base(options)
        {
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<Account> userManager =
                serviceProvider.GetRequiredService<UserManager<Account>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Sesame123#";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                Account user = new Account { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    Description = "SoOoOoo many mushrooms!",
                    Quantity = 50,
                    Cost = 10,
                    StoreId = 1,
                    CategoryId = 1,
                },
                new Product()
                {
                    ProductId = 4,
                    Name = "Buffalo Wings",
                    Description = "Spicy & Delicious!",
                    Quantity = 1000,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 5,
                    Name = "Honey Garlic Wings",
                    Description = "Sweet & Delicious!",
                    Quantity = 500,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 2,
                },
                new Product()
                {
                    ProductId = 6,
                    Name = "Sweet & Spicy Wings",
                    Description = "What a combination!",
                    Quantity = 500,
                    Cost = 5,
                    StoreId = 1,
                    CategoryId = 2,
                }
            );

        }
    }
}
 