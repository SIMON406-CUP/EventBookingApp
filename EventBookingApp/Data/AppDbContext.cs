using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}
