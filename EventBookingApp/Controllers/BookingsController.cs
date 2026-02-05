using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventBookingApp.Data;
using EventBookingApp.Models;

namespace EventBookingApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly EventBookingAppContext _context;

        public BookingsController(EventBookingAppContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = _context.Booking.Include(b => b.Event);
            return View(await bookings.ToListAsync());
        }

        // GET: Bookings/Create/{eventId}
        [HttpGet]
        public IActionResult Create(int eventId)
        {
            var ev = _context.Event.FirstOrDefault(e => e.Id == eventId);
            if (ev == null) return NotFound();

            ViewBag.Event = ev;

            var booking = new Booking
            {
                EventId = ev.Id
            };

            return View(booking);
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,SeatsBooked,EventId")] Booking booking)
        {
            if (!ModelState.IsValid) return View(booking);

            var ev = await _context.Event.FindAsync(booking.EventId);
            if (ev == null)
            {
                ModelState.AddModelError("", "Event not found.");
                return View(booking);
            }

            if (booking.SeatsBooked > ev.AvailableSeats)
            {
                ModelState.AddModelError("", "Not enough seats available.");
                return View(booking);
            }

            // Reduce available seats
            ev.AvailableSeats -= booking.SeatsBooked;

            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", booking.EventId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,SeatsBooked,EventId")] Booking booking)
        {
            if (id != booking.Id) return NotFound();

            if (!ModelState.IsValid) return View(booking);

            try
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Booking.Any(e => e.Id == booking.Id)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Booking
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Booking
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }
    }
}
