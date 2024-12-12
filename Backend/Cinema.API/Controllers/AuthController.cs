using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Cinema.Application.DTOs.User;
using Cinema.Application.Services;
using Cinema.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto.Email, loginDto.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            Response.Cookies.Append("jwt", token, cookieOptions);

            return Ok(new { message = "Login successful" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Role = registerDto.Role
            };

            var result = await _authService.RegisterAsync(user, registerDto.Password);

            if (!result)
            {
                return BadRequest(new { message = "User with this email already exists." });
            }

            return Ok(new { message = "Registration successful" });
        }


        [HttpGet("verify-token")]
        public IActionResult VerifyToken()
        {
            var token = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new
                {
                    isValid = false,
                    message = "No valid session found"
                });
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out _);

                return Ok(new
                {
                    isValid = true,
                    message = "Token is valid"
                });
            }
            catch
            {
                return Unauthorized(new
                {
                    isValid = false,
                    message = "Invalid or expired token"
                });
            }
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {

            // Explicitly set an expired cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1) // Expire now
            };

            Response.Cookies.Append("jwt", "", cookieOptions);
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
