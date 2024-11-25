using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repository
{
    public class ReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.Movie)
                .Include(r => r.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Movie)
                .Include(r => r.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }

            return review;
        }


        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
