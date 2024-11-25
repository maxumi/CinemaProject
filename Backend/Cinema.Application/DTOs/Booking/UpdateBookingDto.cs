using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTOs.Booking
{
    public class UpdateBookingDto
    {
        public int NumberOfTickets { get; set; }
        public int MovieSessionId { get; set; }
        public int PaymentDetailId { get; set; }
    }
}
