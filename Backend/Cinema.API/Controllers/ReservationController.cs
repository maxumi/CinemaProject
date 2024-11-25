using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.Reservation;
using Cinema.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                return Ok(reservation);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Reservation with ID {id} not found." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDto>> CreateReservation([FromBody] CreateReservationDto createReservationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservation = await _reservationService.CreateReservationAsync(createReservationDto);
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto updateReservationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _reservationService.UpdateReservationAsync(id, updateReservationDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Reservation with ID {id} not found." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _reservationService.DeleteReservationAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Reservation with ID {id} not found." });
            }
        }
    }
}
