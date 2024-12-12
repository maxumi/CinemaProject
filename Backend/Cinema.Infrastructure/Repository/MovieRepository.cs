using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repository
{
    public class MovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetLimitedAsync(int limit)
        {
            return await _context.Movies
                .Include(m => m.Genres)
                .OrderBy(m => m.ReleaseDate)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Reviews)
                .Include(m => m.Genres) // Include genres for many-to-many relationship
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovieTitlesAsync()
        {
            return await _context.Movies
                .AsNoTracking()
                .Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title
                })
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Reviews)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task AddWithGenresAsync(Movie movie, List<int> genreIds)
        {
            // Attach genres
            movie.Genres = await _context.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToListAsync();

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWithGenresAsync(Movie movie, List<int> genreIds)
        {
            // Update genres
            var genres = await _context.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToListAsync();

            movie.Genres.Clear(); // Clear existing genres
            movie.Genres = genres;

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
    }
}
