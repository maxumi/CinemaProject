using BCrypt.Net;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Cinema.Application.DTOs.User;

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

            return GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(User user, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return false;
            }

            user.PasswordHash = HashPassword(password);

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

        public async Task<UserDto> GetCurrentUserAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("No valid session found");
            }

            try
            {
                // Validate the token
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true
                };

                var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);

                // Extract user ID from the claims
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                {
                    throw new UnauthorizedAccessException("Invalid token");
                }

                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }

                return new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role
                };
            }
            catch (SecurityTokenExpiredException)
            {
                throw new UnauthorizedAccessException("Token has expired");
            }
            catch (SecurityTokenValidationException)
            {
                throw new UnauthorizedAccessException("Token validation failed");
            }
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
