using Microsoft.AspNetCore.Mvc;
using EventBookingApp.Data;
using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IActionResult Index()
    {
        var upcomingEvents = _context.Events?
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
