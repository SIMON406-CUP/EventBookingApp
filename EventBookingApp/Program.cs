using EventBookingApp.Data;
using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EventBookingAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EventBookingAppContext") ?? throw new InvalidOperationException("Connection string 'EventBookingAppContext' not found.")));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Events.Any())
    {
        context.Events.AddRange(
            new Event { Title = "Music Concert", Location = "City Hall", Date = DateTime.Now.AddDays(5), AvailableSeats = 50 },
            new Event { Title = "Tech Workshop", Location = "Library", Date = DateTime.Now.AddDays(10), AvailableSeats = 30 },
            new Event { Title = "Art Exhibition", Location = "Gallery", Date = DateTime.Now.AddDays(15), AvailableSeats = 20 }
        );
        context.SaveChanges();
    }
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
