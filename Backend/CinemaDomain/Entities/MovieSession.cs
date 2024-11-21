using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Entities
{
    public class MovieSession
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; }

        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }
    }
}
