using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.MovieSession;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class MovieSessionService
    {
        private readonly MovieSessionRepository _movieSessionRepository;

        public MovieSessionService(MovieSessionRepository movieSessionRepository)
        {
            _movieSessionRepository = movieSessionRepository;
        }

        public async Task<IEnumerable<MovieSessionDto>> GetAllMovieSessionsAsync()
        {
            var sessions = await _movieSessionRepository.GetAllAsync();
            return sessions.Select(MapToMovieSessionDto);
        }

        public async Task<MovieSessionDto> GetMovieSessionByIdAsync(int id)
        {
            var session = await _movieSessionRepository.GetByIdAsync(id);
            if (session == null) throw new KeyNotFoundException($"Movie session with ID {id} not found.");

            return MapToMovieSessionDto(session);
        }

        public async Task<MovieSessionDto> CreateMovieSessionAsync(CreateMovieSessionDto createMovieSessionDto)
        {
            var session = MapToMovieSession(createMovieSessionDto);
            await _movieSessionRepository.AddAsync(session);
            return MapToMovieSessionDto(session);
        }

        public async Task UpdateMovieSessionAsync(int id, UpdateMovieSessionDto updateMovieSessionDto)
        {
            var existingSession = await _movieSessionRepository.GetByIdAsync(id);
            if (existingSession == null) throw new KeyNotFoundException($"Movie session with ID {id} not found.");

            existingSession.MovieId = updateMovieSessionDto.MovieId;
            existingSession.CinemaHallId = updateMovieSessionDto.CinemaHallId;
            existingSession.StartTime = updateMovieSessionDto.StartTime;
            existingSession.EndTime = updateMovieSessionDto.EndTime;
            existingSession.Price = updateMovieSessionDto.Price;

            await _movieSessionRepository.UpdateAsync(existingSession);
        }

        public async Task DeleteMovieSessionAsync(int id)
        {
            var session = await _movieSessionRepository.GetByIdAsync(id);
            if (session == null) throw new KeyNotFoundException($"Movie session with ID {id} not found.");

            await _movieSessionRepository.DeleteAsync(session);
        }

        // Mapping methods
        private MovieSessionDto MapToMovieSessionDto(MovieSession session) => new MovieSessionDto
        {
            Id = session.Id,
            MovieId = session.MovieId,
            CinemaHallId = session.CinemaHallId,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Price = session.Price
        };

        private MovieSession MapToMovieSession(CreateMovieSessionDto dto) => new MovieSession
        {
            MovieId = dto.MovieId,
            CinemaHallId = dto.CinemaHallId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Price = dto.Price
        };
    }
}
