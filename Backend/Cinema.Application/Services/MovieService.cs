using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.DTOs;
using Cinema.Application.DTOs.Movie;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository;

        public MovieService(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies.Select(MapToMovieDto);
        }

        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) throw new KeyNotFoundException($"Movie with ID {id} not found.");

            return MapToMovieDto(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto)
        {
            var movie = MapToMovie(createMovieDto);
            await _movieRepository.AddAsync(movie);
            return MapToMovieDto(movie);
        }

        public async Task UpdateMovieAsync(int id, UpdateMovieDto updateMovieDto)
        {
            var existingMovie = await _movieRepository.GetByIdAsync(id);
            if (existingMovie == null) throw new KeyNotFoundException($"Movie with ID {id} not found.");

            existingMovie.Title = updateMovieDto.Title;
            existingMovie.Genre = updateMovieDto.Genre;
            existingMovie.DurationMinutes = updateMovieDto.DurationMinutes;
            existingMovie.ReleaseDate = updateMovieDto.ReleaseDate;
            existingMovie.Description = updateMovieDto.Description;

            await _movieRepository.UpdateAsync(existingMovie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) throw new KeyNotFoundException($"Movie with ID {id} not found.");

            await _movieRepository.DeleteAsync(movie);
        }

        // Mapping methods
        private MovieDto MapToMovieDto(Movie movie) => new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre,
            DurationMinutes = movie.DurationMinutes,
            ReleaseDate = movie.ReleaseDate,
            Description = movie.Description,
            AverageRating = movie.AverageRating
        };

        private Movie MapToMovie(CreateMovieDto dto) => new Movie
        {
            Title = dto.Title,
            Genre = dto.Genre,
            DurationMinutes = dto.DurationMinutes,
            ReleaseDate = dto.ReleaseDate,
            Description = dto.Description
        };
    }
}
