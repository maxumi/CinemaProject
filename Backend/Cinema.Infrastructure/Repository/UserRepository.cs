﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> SearchUsersByNameAsync(string query, int pageNumber, int pageSize)
        {
            query = query.ToLower();
            return await _context.Users
                .AsNoTracking()
                .Where(u => EF.Functions.Like(u.FirstName.ToLower(), $"%{query}%") ||
                            EF.Functions.Like(u.LastName.ToLower(), $"%{query}%") ||
                            EF.Functions.Like((u.FirstName + " " + u.LastName).ToLower(), $"%{query}%"))
                .OrderBy(u => u.FirstName) // Optional: Order by first name
                .Skip((pageNumber - 1) * pageSize) // Skip previous pages
                .Take(pageSize) // Take current page size
                .ToListAsync();
        }



        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
