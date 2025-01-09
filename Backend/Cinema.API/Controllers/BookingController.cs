using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Application.DTOs.Booking;
using Cinema.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly MovieService _movieService;

        public BookingController(BookingService bookingService, MovieService movieService)
        {
            _movieService = movieService;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpPost("new-booking")]
        public async Task<IActionResult> CreateNewBooking([FromBody] CreateDetailedBooking booking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var bookingId = await _bookingService.CreateNewBookingAsync(booking);
                return CreatedAtAction(nameof(GetBooking), new { id = bookingId }, new { id = bookingId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the booking.", details = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id);
                return Ok(booking);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Booking with ID {id} not found." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] CreateBookingDto createBookingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _bookingService.CreateBookingAsync(createBookingDto);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingDto updateBookingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _bookingService.UpdateBookingAsync(id, updateBookingDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Booking with ID {id} not found." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                await _bookingService.DeleteBookingAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Booking with ID {id} not found." });
            }
        }
    }
}
