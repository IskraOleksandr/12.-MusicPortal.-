using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// качаем NuGet Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
// качаем NuGet Package Microsoft.EntityFrameworkCore.SqlServer на третьем уровне для работы с БД
// качаем NuGet Package AutoMapper на втором уровне для преобразования классов моделей в классы DTO
// качаем NuGet Package BCrypt.Net-Next на втором уровне для хеширования
builder.Services.AddMusicPortalContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<ISingerService, SingerService>();
builder.Services.AddTransient<IMusicStyleService, MusicStyleService>();
builder.Services.AddTransient<IMusicService, MusicService>();
builder.Services.AddTransient<IUserService, UserService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Äëèòåëüíîñòü ñåàíñà (òàéì-àóò çàâåðøåíèÿ ñåàíñà)
    options.Cookie.Name = "Session"; // Êàæäàÿ ñåññèÿ èìååò ñâîé èäåíòèôèêàòîð, êîòîðûé ñîõðàíÿåòñÿ â êóêàõ.

});
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Music}/{action=Index}/{id?}");

app.Run();
