using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.DTOs.Movie.Genre;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;

namespace Cinema.Application.Services
{
    public class GenreService
    {
        private readonly GenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(GenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task<GenreDto> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null) throw new KeyNotFoundException($"Genre with ID {id} not found.");

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto)
        {
            var genre = _mapper.Map<Genre>(createGenreDto);
            await _genreRepository.AddAsync(genre);

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task UpdateGenreAsync(int id, UpdateGenreDto updateGenreDto)
        {
            var existingGenre = await _genreRepository.GetByIdAsync(id);
            if (existingGenre == null) throw new KeyNotFoundException($"Genre with ID {id} not found.");

            _mapper.Map(updateGenreDto, existingGenre);
            await _genreRepository.UpdateAsync(existingGenre);
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null) throw new KeyNotFoundException($"Genre with ID {id} not found.");

            await _genreRepository.DeleteAsync(genre);
        }
    }
}
