namespace EventBookingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public int EventId { get; set; }
        public required Event Event { get; set; }
    }

}
