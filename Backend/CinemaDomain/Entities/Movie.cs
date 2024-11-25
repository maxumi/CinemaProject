using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<MovieSession> MovieSessions { get; set; }
        public ICollection<Review> Reviews { get; set; }

        // Average rating calculated from reviews
        public double AverageRating => Reviews?.Any() == true
            ? Reviews.Average(r => r.Rating)
            : 0;
    }
}
