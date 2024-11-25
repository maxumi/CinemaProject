using Cinema.Application.Services;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

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


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // For Service and Repos
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<MovieRepository>();
            builder.Services.AddScoped<MovieService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<ReviewRepository>();
            builder.Services.AddScoped<CinemaHallService>();
            builder.Services.AddScoped<CinemaHallRepository>();
            builder.Services.AddScoped<MovieSessionRepository>();
            builder.Services.AddScoped<MovieSessionService>();
            builder.Services.AddScoped<ReservationRepository>();
            builder.Services.AddScoped<ReservationService>();

            // builder.Services.AddAutoMapper(typeof(Program));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}