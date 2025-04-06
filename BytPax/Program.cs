using BytPax.Instructions;
using BytPax.Models;
using BytPax.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Додано:
builder.Services.AddScoped<IRepository<Article>, ArticleRepository<Article>>();
builder.Services.AddScoped<IRepository<Athlete>, AthleteRepository<Athlete>>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
