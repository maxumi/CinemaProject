using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.DTOs.Movie;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(MovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) throw new KeyNotFoundException($"Movie with ID {id} not found.");

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);

            // Delegate genre handling to the repository
            await _movieRepository.AddWithGenresAsync(movie, createMovieDto.GenreIds);

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task UpdateMovieAsync(int id, UpdateMovieDto updateMovieDto)
        {
            var existingMovie = await _movieRepository.GetByIdAsync(id);
            if (existingMovie == null) throw new KeyNotFoundException($"Movie with ID {id} not found.");

            _mapper.Map(updateMovieDto, existingMovie);

            // Delegate genre handling to the repository
            await _movieRepository.UpdateWithGenresAsync(existingMovie, updateMovieDto.GenreIds);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) throw new KeyNotFoundException($"Movie with ID {id} not found.");

            await _movieRepository.DeleteAsync(movie);
        }
    }
}
