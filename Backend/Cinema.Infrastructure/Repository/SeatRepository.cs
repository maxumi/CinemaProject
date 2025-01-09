using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repository
{
    public class SeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<MovieSession> GetByIdAsync(int id)
        {
            // Load session with related Bookings and their Seats
            return await _context.MovieSessions
                .Include(ms => ms.Bookings)
                    .ThenInclude(b => b.Seats)
                .Include(ms => ms.CinemaHall)
                .FirstOrDefaultAsync(ms => ms.Id == id);
        }

        public async Task<IEnumerable<Seat>> GetSeatsByHallIdAsync(int hallId)
        {
            return await _context.Seats
                .Where(seat => seat.CinemaHallId == hallId)
                .ToListAsync();
        }
    }
}
