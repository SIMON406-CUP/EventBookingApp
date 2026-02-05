using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBookingApp.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }  // Name of the person booking

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }  // Link to the event

        public int SeatsBooked { get; set; }  // How many seats

        public DateTime BookingDate { get; set; } = DateTime.Now;

        // Navigation property
        public Event Event { get; set; }
    }
}
