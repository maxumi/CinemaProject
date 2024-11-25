using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTOs.Reservation
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NumberOfTickets { get; set; }
        public int MovieSessionId { get; set; }
        public int PaymentDetailId { get; set; }
        public List<int> SeatIds { get; set; }
    }
}
