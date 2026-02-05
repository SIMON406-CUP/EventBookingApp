using Microsoft.AspNetCore.Mvc;
using EventBookingApp.Data;
using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly EventBookingAppContext _context;

    public HomeController(EventBookingAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IActionResult Index()
    {
        var upcomingEvents = _context.Event?
                                     .Where(e => e.Date >= DateTime.Now)
                                     .OrderBy(e => e.Date)
                                     .Take(3)
                                     .ToList()
                                     ?? new List<Event>();

        return View(upcomingEvents);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
