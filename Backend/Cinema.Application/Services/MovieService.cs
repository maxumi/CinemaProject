using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.DTOs.CinemaHall;
using Cinema.Application.DTOs.Movie;
using Cinema.Application.DTOs.MovieSession;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository;
        private readonly MovieSessionRepository _movieSessionRepository;
        private readonly CinemaHallRepository _cinemaHallRepository;
        private readonly IMapper _mapper;

        public MovieService(
            MovieRepository movieRepository,
            MovieSessionRepository movieSessionRepository,
            CinemaHallRepository cinemaHallRepository,
            IMapper mapper)
        {
            _movieRepository = movieRepository;
            _movieSessionRepository = movieSessionRepository;
            _cinemaHallRepository = cinemaHallRepository;
            _mapper = mapper;
        }

        public async Task<object> GetFrontPageAsync(int moviesPage, int moviesAmount, int sessionsPage, int sessionsAmount)
        {
            // Fetch paginated movies
            var allMovies = await _movieRepository.GetAllAsync();
            var paginatedMovies = allMovies
                .Skip((moviesPage - 1) * moviesAmount)
                .Take(moviesAmount)
                .ToList();
            var hasMoreMovies = allMovies.Skip(moviesPage * moviesAmount).Any();
            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(paginatedMovies);

            // Fetch paginated movie sessions
            var allSessions = await _movieSessionRepository.GetAllAsync();
            var paginatedSessions = allSessions
                .Skip((sessionsPage - 1) * sessionsAmount)
                .Take(sessionsAmount)
                .ToList();
            var hasMoreSessions = allSessions.Skip(sessionsPage * sessionsAmount).Any();
            var movieSessionDtos = _mapper.Map<IEnumerable<MovieSessionDto>>(paginatedSessions);

            // Fetch relevant cinema halls based on paginated sessions
            var cinemaHallIds = paginatedSessions.Select(ms => ms.CinemaHallId).Distinct().ToList();
            var relevantCinemaHalls = await _cinemaHallRepository.GetByIdsAsync(cinemaHallIds);
            var cinemaHallDtos = _mapper.Map<IEnumerable<CinemaHallDto>>(relevantCinemaHalls);

            return new
            {
                Movies = new
                {
                    Items = movieDtos,
                    HasMore = hasMoreMovies
                },
                MovieSessions = new
                {
                    Items = movieSessionDtos,
                    HasMore = hasMoreSessions
                },
                CinemaHalls = cinemaHallDtos
            };
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
