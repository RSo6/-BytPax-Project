using BytPax.Data;
using BytPax.Data.Database;
using BytPax.Instructions;
using BytPax.Models;
using BytPax.Models.core;
using BytPax.Repositories;
using BytPax.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new UserJsonConverter());
});

builder.Services.AddSingleton<IDataStorage<Article>>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<JsonStorage<Article>>>();
    return new JsonStorage<Article>("Data/articles.json", logger);
});

builder.Services.AddSingleton<IDataStorage<HistoricalEvent>>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<JsonStorage<HistoricalEvent>>>();
    return new JsonStorage<HistoricalEvent>("Data/historicalEvents.json", logger);
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

builder.Services.AddSingleton<IDataStorage<User>>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<JsonStorage<User>>>();
    return new JsonStorage<User>("Data/users.json", logger);
});

builder.Services.AddSingleton<IDataStorage<Sport>>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<JsonStorage<Sport>>>();
    return new JsonStorage<Sport>("Data/sports.json", logger);
});

builder.Services.AddSingleton<IDataStorage<RecordHistory>>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<JsonStorage<RecordHistory>>>();
    return new JsonStorage<RecordHistory>("Data/recordsHistory.json", logger);
});

// Json 
builder.Services.AddSingleton(typeof(IDataStorage<>), typeof(JsonStorage<>));

// EF
// builder.Services.AddScoped(typeof(IDataStorage<>), typeof(EfStorage<>));
    
builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));

builder.Services.AddScoped<SearchService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();  
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




