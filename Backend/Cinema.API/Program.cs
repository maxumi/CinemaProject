using System.Text;
using AutoMapper;
using Cinema.Application;
using Cinema.Application.Services;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Cinema.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register AppDbContext with DI
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Controllers and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Swagger configuration for JWT and Cookie debugging
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinema API", Version = "v1" });

                // JWT Token Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token in the format: Bearer {token}"
                });

                // Cookie Authentication
                c.AddSecurityDefinition("CookieAuth", new OpenApiSecurityScheme
                {
                    Name = "Cookie",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Enter your cookies in the format: jwt={token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    },
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "CookieAuth"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Add CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", corsBuilder =>
                {
                    corsBuilder.WithOrigins("http://localhost:4200") // Replace with actual frontend URL
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials(); // Allow cookies in CORS requests
                });
            });

            // Register Services and Repositories
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<MovieRepository>();
            builder.Services.AddScoped<MovieService>();
            builder.Services.AddScoped<GenreRepository>();
            builder.Services.AddScoped<GenreService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<ReviewRepository>();
            builder.Services.AddScoped<CinemaHallService>();
            builder.Services.AddScoped<CinemaHallRepository>();
            builder.Services.AddScoped<MovieSessionRepository>();
            builder.Services.AddScoped<MovieSessionService>();
            builder.Services.AddScoped<BookingRepository>();
            builder.Services.AddScoped<BookingService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configure JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new Exception("JWT SecretKey is not configured in appsettings.json");
            }

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                        ValidAudience = jwtSettings.GetValue<string>("Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                // Production-only configurations
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthentication(); // Ensure this is added before UseAuthorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
