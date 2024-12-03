using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repository
{
    public class CinemaHallRepository
    {
        private readonly AppDbContext _context;

        public CinemaHallRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CinemaHall>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.CinemaHalls
                .Where(ch => ids.Contains(ch.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<CinemaHall>> GetAllAsync()
        {
            return await _context.CinemaHalls
                .Include(ch => ch.MovieSessions)
                .Include(ch => ch.Seats)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CinemaHall> GetByIdAsync(int id)
        {
            return await _context.CinemaHalls
                .Include(ch => ch.MovieSessions)
                .Include(ch => ch.Seats)
                .FirstOrDefaultAsync(ch => ch.Id == id);
        }

        public async Task AddAsync(CinemaHall cinemaHall)
        {
            await _context.CinemaHalls.AddAsync(cinemaHall);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CinemaHall cinemaHall)
        {
            _context.CinemaHalls.Update(cinemaHall);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CinemaHall cinemaHall)
        {
            _context.CinemaHalls.Remove(cinemaHall);
            await _context.SaveChangesAsync();
        }
    }
}
