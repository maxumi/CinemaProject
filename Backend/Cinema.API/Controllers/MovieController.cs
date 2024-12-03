using Cinema.Application.DTOs.Movie;
using Cinema.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("FrontPage")]
        public async Task<IActionResult> GetFrontPage(
            [FromQuery] int moviesPage = 1,
            [FromQuery] int moviesAmount = 5,
            [FromQuery] int sessionsPage = 1,
            [FromQuery] int sessionsAmount = 10)
        {
            if (moviesPage <= 0 || moviesAmount <= 0 || sessionsPage <= 0 || sessionsAmount <= 0)
                return BadRequest(new { message = "Page and amount values must be greater than 0." });

            var result = await _movieService.GetFrontPageAsync(moviesPage, moviesAmount, sessionsPage, sessionsAmount);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(id);
                return Ok(movie);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Movie with ID {id} not found." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie([FromBody] CreateMovieDto createMovieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = await _movieService.CreateMovieAsync(createMovieDto);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieDto updateMovieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _movieService.UpdateMovieAsync(id, updateMovieDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Movie with ID {id} not found." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Movie with ID {id} not found." });
            }
        }
    }
}
