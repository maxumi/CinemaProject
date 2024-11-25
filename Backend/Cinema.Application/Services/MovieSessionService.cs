using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.DTOs.MovieSession;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class MovieSessionService
    {
        private readonly MovieSessionRepository _movieSessionRepository;
        private readonly IMapper _mapper;

        public MovieSessionService(MovieSessionRepository movieSessionRepository, IMapper mapper)
        {
            _movieSessionRepository = movieSessionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieSessionDto>> GetAllMovieSessionsAsync()
        {
            var sessions = await _movieSessionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MovieSessionDto>>(sessions);
        }

        public async Task<MovieSessionDto> GetMovieSessionByIdAsync(int id)
        {
            var session = await _movieSessionRepository.GetByIdAsync(id);
            if (session == null) throw new KeyNotFoundException($"Movie session with ID {id} not found.");

            return _mapper.Map<MovieSessionDto>(session);
        }

        public async Task<MovieSessionDto> CreateMovieSessionAsync(CreateMovieSessionDto createMovieSessionDto)
        {
            var session = _mapper.Map<MovieSession>(createMovieSessionDto);
            await _movieSessionRepository.AddAsync(session);

            return _mapper.Map<MovieSessionDto>(session);
        }

        public async Task UpdateMovieSessionAsync(int id, UpdateMovieSessionDto updateMovieSessionDto)
        {
            var existingSession = await _movieSessionRepository.GetByIdAsync(id);
            if (existingSession == null) throw new KeyNotFoundException($"Movie session with ID {id} not found.");

            _mapper.Map(updateMovieSessionDto, existingSession);
            await _movieSessionRepository.UpdateAsync(existingSession);
        }

        public async Task DeleteMovieSessionAsync(int id)
        {
            var session = await _movieSessionRepository.GetByIdAsync(id);
            if (session == null) throw new KeyNotFoundException($"Movie session with ID {id} not found.");

            await _movieSessionRepository.DeleteAsync(session);
        }
    }
}
