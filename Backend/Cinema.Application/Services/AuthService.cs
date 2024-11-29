using BCrypt.Net;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Application.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null; // Invalid credentials
            }

            // Generate JWT token
            return GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(User user, string password)
        {
            // Check if user with the same email already exists
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return false; // Email already exists
            }

            // Hash the password
            user.PasswordHash = HashPassword(password);

            // Save the user to the database
            await _userRepository.AddAsync(user);

            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            // Hash the password using BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Verify the password using BCrypt
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
