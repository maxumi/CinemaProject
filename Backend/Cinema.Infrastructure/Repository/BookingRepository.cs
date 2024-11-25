using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repository
{
    public class BookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(r => r.User)
                .Include(r => r.MovieSession)
                .Include(r => r.PaymentDetail)
                .Include(r => r.Seats)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(r => r.User)
                .Include(r => r.MovieSession)
                .Include(r => r.PaymentDetail)
                .Include(r => r.Seats)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Booking Booking )
        {
            await _context.Bookings.AddAsync(Booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
