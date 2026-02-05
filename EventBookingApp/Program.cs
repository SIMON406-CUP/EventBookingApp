using EventBookingApp.Data;
using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Add services
// --------------------

// Register DbContext (only once)
builder.Services.AddDbContext<EventBookingAppContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EventBookingAppContext")
        ?? throw new InvalidOperationException("Connection string 'EventBookingAppContext' not found.")
    ));

// Add MVC controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --------------------
// Configure middleware
// --------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// --------------------
// Seed initial data
// --------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EventBookingAppContext>();

    // DELETE ALL EVENTS
    context.Event.RemoveRange(context.Event);
    context.SaveChanges();

    // ADD FRESH EVENTS
    context.Event.AddRange(
        new Event { Title = "Music Concert", Location = "City Hall", Date = DateTime.Now.AddDays(5), AvailableSeats = 50 },
        new Event { Title = "Tech Workshop", Location = "Library", Date = DateTime.Now.AddDays(10), AvailableSeats = 30 },
        new Event { Title = "Art Exhibition", Location = "Gallery", Date = DateTime.Now.AddDays(15), AvailableSeats = 20 }
    );

    context.SaveChanges();
}

// --------------------
// Map routes
// --------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
