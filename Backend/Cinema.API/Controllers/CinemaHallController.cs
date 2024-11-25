using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.CinemaHall;
using Cinema.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaHallController : ControllerBase
    {
        private readonly CinemaHallService _cinemaHallService;

        public CinemaHallController(CinemaHallService cinemaHallService)
        {
            _cinemaHallService = cinemaHallService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CinemaHallDto>>> GetCinemaHalls()
        {
            var cinemaHalls = await _cinemaHallService.GetAllCinemaHallsAsync();
            return Ok(cinemaHalls);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CinemaHallDto>> GetCinemaHall(int id)
        {
            try
            {
                var cinemaHall = await _cinemaHallService.GetCinemaHallByIdAsync(id);
                return Ok(cinemaHall);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Cinema hall with ID {id} not found." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CinemaHallDto>> CreateCinemaHall([FromBody] CreateCinemaHallDto createCinemaHallDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cinemaHall = await _cinemaHallService.CreateCinemaHallAsync(createCinemaHallDto);
            return CreatedAtAction(nameof(GetCinemaHall), new { id = cinemaHall.Id }, cinemaHall);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCinemaHall(int id, [FromBody] UpdateCinemaHallDto updateCinemaHallDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _cinemaHallService.UpdateCinemaHallAsync(id, updateCinemaHallDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Cinema hall with ID {id} not found." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinemaHall(int id)
        {
            try
            {
                await _cinemaHallService.DeleteCinemaHallAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Cinema hall with ID {id} not found." });
            }
        }
    }
}
