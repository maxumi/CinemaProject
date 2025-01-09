using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Domain.Entities;

namespace Cinema.Application.DTOs.Booking
{

    public class PaymentDetailDto
    {
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime Date { get; set; }
        public int BookingId { get; set; }
    }
    public class CreateDetailedBooking
    {
        public int UserId { get; set; }
        public int NumberOfTickets { get; set; }
        public int MovieSessionId { get; set; }
        public List<int> SeatIds { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingDate { get; set; }
        public PaymentDetailDto PaymentDetail { get; set; }
    }

}
