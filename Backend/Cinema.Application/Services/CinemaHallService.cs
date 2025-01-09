using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.DTOs.CinemaHall;
using Cinema.Application.DTOs.CinemaHall.Seat;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class CinemaHallService
    {
        private readonly CinemaHallRepository _cinemaHallRepository;
        private readonly IMapper _mapper;

        public CinemaHallService(CinemaHallRepository cinemaHallRepository, IMapper mapper)
        {
            _cinemaHallRepository = cinemaHallRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SeatDto>> GetSeatsByHallIdAsync(int hallId)
        {
            var hall = await _cinemaHallRepository.GetByIdAsync(hallId);
            if (hall == null) throw new KeyNotFoundException($"hall not found.");

            return hall.Seats.Select(seat => new SeatDto
            {
                Id = seat.Id,
                SeatNumber = seat.SeatNumber,
                CinemaHallId = seat.CinemaHallId
            });
        }


        public async Task<IEnumerable<CinemaHallDto>> GetAllCinemaHallsAsync()
        {
            var cinemaHalls = await _cinemaHallRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CinemaHallDto>>(cinemaHalls);
        }

        public async Task<CinemaHallDto> GetCinemaHallByIdAsync(int id)
        {
            var cinemaHall = await _cinemaHallRepository.GetByIdAsync(id);
            if (cinemaHall == null) throw new KeyNotFoundException($"Cinema hall with ID {id} not found.");

            return _mapper.Map<CinemaHallDto>(cinemaHall);
        }

        public async Task<CinemaHallDto> CreateCinemaHallAsync(CreateCinemaHallDto createCinemaHallDto)
        {
            var cinemaHall = _mapper.Map<CinemaHall>(createCinemaHallDto);
            await _cinemaHallRepository.AddAsync(cinemaHall);

            return _mapper.Map<CinemaHallDto>(cinemaHall);
        }

        public async Task UpdateCinemaHallAsync(int id, UpdateCinemaHallDto updateCinemaHallDto)
        {
            var existingCinemaHall = await _cinemaHallRepository.GetByIdAsync(id);
            if (existingCinemaHall == null) throw new KeyNotFoundException($"Cinema hall with ID {id} not found.");

            _mapper.Map(updateCinemaHallDto, existingCinemaHall);
            await _cinemaHallRepository.UpdateAsync(existingCinemaHall);
        }

        public async Task DeleteCinemaHallAsync(int id)
        {
            var cinemaHall = await _cinemaHallRepository.GetByIdAsync(id);
            if (cinemaHall == null) throw new KeyNotFoundException($"Cinema hall with ID {id} not found.");

            await _cinemaHallRepository.DeleteAsync(cinemaHall);
        }
    }
}
