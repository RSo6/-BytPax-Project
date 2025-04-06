using BytPax.Instructions;
using BytPax.Models;
using BytPax.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Додаємо підтримку MVC
builder.Services.AddControllersWithViews();

// Реєструємо репозиторії
builder.Services.AddScoped<IRepository<Article>, ArticleRepository<Article>>();
builder.Services.AddScoped<IRepository<Athlete>, AthleteRepository<Athlete>>();

var app = builder.Build();

// Обробка помилок
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//  Встановлюємо маршрут за замовчуванням: Admin/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}"); // <<== ОЦЕ ГОЛОВНЕ

app.Run();
