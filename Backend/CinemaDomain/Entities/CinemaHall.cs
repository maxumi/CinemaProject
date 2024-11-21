using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public ICollection<MovieSession> MovieSessions { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
