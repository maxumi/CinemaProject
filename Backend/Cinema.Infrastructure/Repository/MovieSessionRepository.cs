using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repository
{
    public class MovieSessionRepository
    {
        private readonly AppDbContext _context;

        public MovieSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieSession>> GetLimitedAsync(int limit)
        {
            return await _context.MovieSessions
                .Include(ms => ms.Movie)
                .Include(ms => ms.CinemaHall)
                .OrderBy(ms => ms.StartTime)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<MovieSession>> GetAllAsync()
        {
            return await _context.MovieSessions
                .Include(ms => ms.Movie)
                .Include(ms => ms.CinemaHall)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MovieSession> GetByIdAsync(int id)
        {
            return await _context.MovieSessions
                .Include(ms => ms.Movie)
                .Include(ms => ms.CinemaHall)
                .FirstOrDefaultAsync(ms => ms.Id == id);
        }

        public async Task AddAsync(MovieSession session)
        {
            await _context.MovieSessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MovieSession session)
        {
            _context.MovieSessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MovieSession session)
        {
            _context.MovieSessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
}
