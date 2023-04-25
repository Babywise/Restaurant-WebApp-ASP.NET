using Meal_Ordering_Class_Library.Services;
using Meal_Ordering_Customer.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services and configure options
builder.Services.AddHttpClient();
//builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MealOrderingApp.Customer.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<BaseJwtService>();

// Configure authentication middleware
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["ApiSettings:ApiBaseUrl"];
    options.Audience = builder.Configuration["ApiSettings:ApiBaseUrl"];
});*/

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

app.UseSession();

//app.UseAuthentication();
app.UseStatusCodePagesWithReExecute("/Home/AccessDenied/{0}");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    var jwtToken = string.Empty;
    if (context.Session != null && context.Session.TryGetValue("Authorization", out var tokenBytes))
        jwtToken = Encoding.UTF8.GetString(tokenBytes);

    if (!string.IsNullOrEmpty(jwtToken))
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        if (token.ValidTo < DateTime.UtcNow)
        {
            context.Session.Clear();
        }
    }
    await next.Invoke();
});

app.Run($"https://0.0.0.0:7206");
