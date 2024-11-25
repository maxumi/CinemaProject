using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.CinemaHall;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class CinemaHallService
    {
        private readonly CinemaHallRepository _cinemaHallRepository;

        public CinemaHallService(CinemaHallRepository cinemaHallRepository)
        {
            _cinemaHallRepository = cinemaHallRepository;
        }

        public async Task<IEnumerable<CinemaHallDto>> GetAllCinemaHallsAsync()
        {
            var cinemaHalls = await _cinemaHallRepository.GetAllAsync();
            return cinemaHalls.Select(MapToCinemaHallDto);
        }

        public async Task<CinemaHallDto> GetCinemaHallByIdAsync(int id)
        {
            var cinemaHall = await _cinemaHallRepository.GetByIdAsync(id);
            if (cinemaHall == null) throw new KeyNotFoundException($"Cinema hall with ID {id} not found.");

            return MapToCinemaHallDto(cinemaHall);
        }

        public async Task<CinemaHallDto> CreateCinemaHallAsync(CreateCinemaHallDto createCinemaHallDto)
        {
            var cinemaHall = MapToCinemaHall(createCinemaHallDto);
            await _cinemaHallRepository.AddAsync(cinemaHall);
            return MapToCinemaHallDto(cinemaHall);
        }

        public async Task UpdateCinemaHallAsync(int id, UpdateCinemaHallDto updateCinemaHallDto)
        {
            var existingCinemaHall = await _cinemaHallRepository.GetByIdAsync(id);
            if (existingCinemaHall == null) throw new KeyNotFoundException($"Cinema hall with ID {id} not found.");

            existingCinemaHall.Name = updateCinemaHallDto.Name;
            existingCinemaHall.Capacity = updateCinemaHallDto.Capacity;

            await _cinemaHallRepository.UpdateAsync(existingCinemaHall);
        }

        public async Task DeleteCinemaHallAsync(int id)
        {
            var cinemaHall = await _cinemaHallRepository.GetByIdAsync(id);
            if (cinemaHall == null) throw new KeyNotFoundException($"Cinema hall with ID {id} not found.");

            await _cinemaHallRepository.DeleteAsync(cinemaHall);
        }

        // Mapping methods
        private CinemaHallDto MapToCinemaHallDto(CinemaHall cinemaHall) => new CinemaHallDto
        {
            Id = cinemaHall.Id,
            Name = cinemaHall.Name,
            Capacity = cinemaHall.Capacity
        };

        private CinemaHall MapToCinemaHall(CreateCinemaHallDto dto) => new CinemaHall
        {
            Name = dto.Name,
            Capacity = dto.Capacity
        };
    }
}
