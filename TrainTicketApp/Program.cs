using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TrainTicketApp.Services;
using TrainTicketApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Adatbázis konfiguráció
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Szolgáltatások regisztrálása
builder.Services.AddTransient<ITrainService, TrainService>();
builder.Services.AddTransient<IOrderService, OrderService>();

// Razor Pages engedélyezése
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
