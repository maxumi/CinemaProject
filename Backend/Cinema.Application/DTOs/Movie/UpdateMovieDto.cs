using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTOs.Movie
{
    public class UpdateMovieDto
    {
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [Range(0, 600, ErrorMessage = "Duration must be between 0 and 600 minutes.")]
        public int DurationMinutes { get; set; }

        public DateTime ReleaseDate { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        public List<int> GenreIds { get; set; } // Genre IDs
    }
}
