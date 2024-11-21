using System.Collections.Generic;

namespace Cinema.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int NumberOfTickets { get; set; }
        public int MovieSessionId { get; set; }
        public MovieSession MovieSession { get; set; }
        public int PaymentDetailId { get; set; }
        public PaymentDetail PaymentDetail { get; set; }

        // Many-to-Many Relationship with Seats
        public ICollection<Seat> Seats { get; set; }
    }
}
