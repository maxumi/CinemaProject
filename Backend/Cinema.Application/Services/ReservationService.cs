using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.Reservation;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;

        public ReservationService(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return reservations.Select(MapToReservationDto);
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null) throw new KeyNotFoundException($"Reservation with ID {id} not found.");

            return MapToReservationDto(reservation);
        }

        public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            var reservation = MapToReservation(createReservationDto);
            await _reservationRepository.AddAsync(reservation);
            return MapToReservationDto(reservation);
        }

        public async Task UpdateReservationAsync(int id, UpdateReservationDto updateReservationDto)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(id);
            if (existingReservation == null) throw new KeyNotFoundException($"Reservation with ID {id} not found.");

            existingReservation.NumberOfTickets = updateReservationDto.NumberOfTickets;
            existingReservation.MovieSessionId = updateReservationDto.MovieSessionId;
            existingReservation.PaymentDetailId = updateReservationDto.PaymentDetailId;

            await _reservationRepository.UpdateAsync(existingReservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null) throw new KeyNotFoundException($"Reservation with ID {id} not found.");

            await _reservationRepository.DeleteAsync(reservation);
        }

        // Mapping methods
        private ReservationDto MapToReservationDto(Reservation reservation) => new ReservationDto
        {
            Id = reservation.Id,
            UserId = reservation.UserId,
            NumberOfTickets = reservation.NumberOfTickets,
            MovieSessionId = reservation.MovieSessionId,
            PaymentDetailId = reservation.PaymentDetailId,
            SeatIds = reservation.Seats?.Select(s => s.Id).ToList()
        };

        private Reservation MapToReservation(CreateReservationDto dto) => new Reservation
        {
            UserId = dto.UserId,
            NumberOfTickets = dto.NumberOfTickets,
            MovieSessionId = dto.MovieSessionId,
            PaymentDetailId = dto.PaymentDetailId,
            Seats = dto.SeatIds?.Select(id => new Seat { Id = id }).ToList()
        };
    }
}
