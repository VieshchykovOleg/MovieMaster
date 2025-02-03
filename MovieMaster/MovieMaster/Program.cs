using MovieMaster.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Налаштування контексту БД
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Налаштування аутентифікації через Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login"; // Куди редиректить, якщо не авторизований
        options.LogoutPath = "/Users/Logout"; // Куди редиректить після виходу
        options.AccessDeniedPath = "/Home/AccessDenied"; // Шлях у випадку доступу без авторизації
    });

builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Тривалість сесії
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Додавання аутентифікації і авторизації
app.UseAuthentication();
app.UseAuthorization();

// Використання сесій
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{controller=Index}/{action=Index}/{id?}"
);

app.Run();
