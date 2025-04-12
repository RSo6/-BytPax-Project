using BytPax.Instructions;
using BytPax.Models;
using BytPax.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ������ �������� MVC
builder.Services.AddControllersWithViews();

// �������� ���������
builder.Services.AddScoped<IRepository<Article>, ArticleRepository<Article>>();
builder.Services.AddScoped<IRepository<Athlete>, AthleteRepository<Athlete>>();

var app = builder.Build();

// ������� �������
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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
