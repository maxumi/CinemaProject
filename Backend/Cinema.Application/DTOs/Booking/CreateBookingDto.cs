namespace Cinema.Application.DTOs.Booking
{
    public class CreateBookingDto
    {
        public int UserId { get; set; }
        public int NumberOfTickets { get; set; }
        public int MovieSessionId { get; set; }
        public int PaymentDetailId { get; set; }
        public List<int> SeatIds { get; set; }
    }
}
