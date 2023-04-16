using Meal_Ordering_API.DataAccess;
using Meal_Ordering_API.Services;
using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.Services;
using Meal_Ordering_WebApp.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/Home/Index", () => "Hello World!").RequireAuthorization();
app.Run();
/*
var builder = WebApplication.CreateBuilder(args);

// Configure JWT authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.
builder.Services.AddControllersWithViews();
var connStr = builder.Configuration.GetConnectionString("MealOrderingDb");
builder.Services.AddDbContext<MealOrderingAPIContext>(options => options.UseSqlServer(connStr));
builder.Services.AddScoped<IMealOrderingService, DbMealOrderingService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<MealOrderingAPIContext>().AddDefaultTokenProviders();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{start=l}");

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    //create default superuser and custom roles
    await MealOrderingAPIContext.IntitalizeUserIdentities(scope.ServiceProvider);
}

app.Run();
*/