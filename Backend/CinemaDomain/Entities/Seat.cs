using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; }

        // Many to Many
        public ICollection<Reservation> Reservations { get; set; }


    }
}
