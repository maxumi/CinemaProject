using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.MovieSession;
using Cinema.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieSessionController : ControllerBase
    {
        private readonly MovieSessionService _movieSessionService;

        public MovieSessionController(MovieSessionService movieSessionService)
        {
            _movieSessionService = movieSessionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieSessionDto>>> GetMovieSessions()
        {
            var sessions = await _movieSessionService.GetAllMovieSessionsAsync();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieSessionDto>> GetMovieSession(int id)
        {
            try
            {
                var session = await _movieSessionService.GetMovieSessionByIdAsync(id);
                return Ok(session);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Movie session with ID {id} not found." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<MovieSessionDto>> CreateMovieSession([FromBody] CreateMovieSessionDto createMovieSessionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = await _movieSessionService.CreateMovieSessionAsync(createMovieSessionDto);
            return CreatedAtAction(nameof(GetMovieSession), new { id = session.Id }, session);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieSession(int id, [FromBody] UpdateMovieSessionDto updateMovieSessionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _movieSessionService.UpdateMovieSessionAsync(id, updateMovieSessionDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Movie session with ID {id} not found." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieSession(int id)
        {
            try
            {
                await _movieSessionService.DeleteMovieSessionAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Movie session with ID {id} not found." });
            }
        }
    }
}
