using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yurukcu.Web.Data;
using Yurukcu.Web.Entity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dakika sonra oturumu kapat
    options.Cookie.HttpOnly = true; // JavaScript'in eri�imini engelle
    options.Cookie.IsEssential = true; // Kullan�c� izin verse de vermese de �erezlerin kullan�lmas�n� sa�lar
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Sadece HTTPS �zerinden g�nder
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

DataSeeding.Seed(app);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
