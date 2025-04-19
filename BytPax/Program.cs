using BytPax.Data;
using BytPax.Instructions;
using BytPax.Models;
using BytPax.Repositories;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); 

builder.Services.AddSingleton<IDataStorage<Article>>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<JsonStorage<Article>>>();
    return new JsonStorage<Article>("Data/articles.json", logger);
});

builder.Services.AddSingleton<IDataStorage<Athlete>>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<JsonStorage<Athlete>>>();
    return new JsonStorage<Athlete>("Data/athletes.json", logger);  
});

builder.Services.AddSingleton<IDataStorage<Category>>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<JsonStorage<Category>>>();
    return new JsonStorage<Category>("Data/categories.json", logger);  
});

builder.Services.AddScoped(typeof(Repository<>));
builder.Services.AddScoped<Repository<Category>>();


var app = builder.Build();

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
    name: "admin", 
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Admin" });

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




