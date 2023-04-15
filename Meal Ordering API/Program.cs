using Meal_Ordering_API.Services;
using Meal_Ordering_Class_Library.Services;
using Meal_Ordering_WebApp.Entities;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connStr = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<MealOrderingAPIContext>(options => options.UseSqlServer(connStr));
builder.Services.AddScoped<IMealOrderingService, DbMealOrderingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{start=l}");

app.Run();
