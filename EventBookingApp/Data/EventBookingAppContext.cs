using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventBookingApp.Models;

namespace EventBookingApp.Data
{
    public class EventBookingAppContext : DbContext
    {
        public EventBookingAppContext (DbContextOptions<EventBookingAppContext> options)
            : base(options)
        {
        }

        public DbSet<EventBookingApp.Models.Event> Event { get; set; } = default!;
        public DbSet<EventBookingApp.Models.Booking> Booking { get; set; } = default!;
    }
}
