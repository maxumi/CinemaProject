using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTOs.MovieSession
{
    public class MovieSessionDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CinemaHallId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
    }
}
