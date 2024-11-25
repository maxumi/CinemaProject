using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.DTOs.Booking;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(BookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) throw new KeyNotFoundException($"Booking with ID {id} not found.");

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var booking = _mapper.Map<Booking>(createBookingDto);
            await _bookingRepository.AddAsync(booking);

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task UpdateBookingAsync(int id, UpdateBookingDto updateBookingDto)
        {
            var existingBooking = await _bookingRepository.GetByIdAsync(id);
            if (existingBooking == null) throw new KeyNotFoundException($"Booking with ID {id} not found.");

            _mapper.Map(updateBookingDto, existingBooking);
            await _bookingRepository.UpdateAsync(existingBooking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) throw new KeyNotFoundException($"Booking with ID {id} not found.");

            await _bookingRepository.DeleteAsync(booking);
        }
    }
}
