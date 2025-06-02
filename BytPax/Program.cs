using BytPax.Data.Database;
using BytPax.Instructions;
using BytPax.Models;
using BytPax.Repositories;
using BytPax.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });

// EF
builder.Services.AddScoped(typeof(IDataStorage<>), typeof(EfStorage<>));
builder.Services.AddScoped(typeof(Repository<>));
builder.Services.AddScoped<SearchService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    // Ініціалізація базових категорій, якщо їх нема
    if (!db.Categories.Any())
    {
        var categories = new List<Category>
        {
            new Category { Name = "Футбол", TypeOfSport = "Командний", Description = "Командний вид спорту з м’ячем" },
            new Category { Name = "Біг", TypeOfSport = "Індивідуальний", Description = "Індивідуальний вид спорту на швидкість" },
            new Category { Name = "Плавання", TypeOfSport = "Індивідуальний", Description = "Водний вид спорту" },
            new Category { Name = "Теніс", TypeOfSport = "Ракетковий", Description = "Спорт з ракеткою і м’ячем" },
            new Category { Name = "Баскетбол", TypeOfSport = "Командний", Description = "Командний вид спорту з м’ячем" }
        };

        db.Categories.AddRange(categories);
        db.SaveChanges();
    }
}

app.UseAuthentication();
app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();