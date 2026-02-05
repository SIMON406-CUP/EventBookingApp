using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventBookingApp.Data;
using EventBookingApp.Models;
using System.Linq;

public class HomeController : Controller
{
    private readonly EventBookingAppContext _context;

    public HomeController(EventBookingAppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Get upcoming events (today or later), ordered by date
        var upcomingEvents = _context.Event
                                     .Where(e => e.Date >= DateTime.Now)
                                     .OrderBy(e => e.Date)
                                     .ToList();

        return View(upcomingEvents); // Pass to the view
    }
}
