using Cinema.Domain.Entities;
using Cinema.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Cinema.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seeding CinemaHall
            modelBuilder.Entity<CinemaHall>().HasData(
                new CinemaHall { Id = 1, Name = "Hall A", Capacity = 150 },
                new CinemaHall { Id = 2, Name = "Hall B", Capacity = 100 }
            );

            // Seeding Seats
            modelBuilder.Entity<Seat>().HasData(
                new Seat { Id = 1, SeatNumber = "A1", CinemaHallId = 1 },
                new Seat { Id = 2, SeatNumber = "A2", CinemaHallId = 1 },
                new Seat { Id = 3, SeatNumber = "B1", CinemaHallId = 2 },
                new Seat { Id = 4, SeatNumber = "B2", CinemaHallId = 2 }
            );

            // Seeding Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" }
            );

            // Seeding Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "The Action Hero",
                    DurationMinutes = 120,
                    ReleaseDate = new DateTime(2023, 6, 15),
                    Description = "An action-packed thriller about a hero saving the day."
                },
                new Movie
                {
                    Id = 2,
                    Title = "Comedy Nights",
                    DurationMinutes = 90,
                    ReleaseDate = new DateTime(2023, 8, 10),
                    Description = "A hilarious comedy to keep you entertained."
                }
            );

            // Mapping Genres to Movies
            modelBuilder.Entity("MovieGenre").HasData(
                new { MovieId = 1, GenreId = 1 }, // The Action Hero -> Action
                new { MovieId = 2, GenreId = 2 }  // Comedy Nights -> Comedy
            );

            // Seeding MovieSessions
            modelBuilder.Entity<MovieSession>().HasData(
                new MovieSession
                {
                    Id = 1,
                    MovieId = 1,
                    CinemaHallId = 1,
                    StartTime = new DateTime(2024, 12, 1, 18, 0, 0),
                    EndTime = new DateTime(2024, 12, 1, 20, 0, 0),
                    Price = 12.50m
                },
                new MovieSession
                {
                    Id = 2,
                    MovieId = 2,
                    CinemaHallId = 2,
                    StartTime = new DateTime(2024, 12, 2, 15, 0, 0),
                    EndTime = new DateTime(2024, 12, 2, 16, 30, 0),
                    Price = 10.00m
                }
            );

            // Seeding Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PasswordHash = "hashedpassword1",
                    Role = Role.Customer
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PasswordHash = "hashedpassword2",
                    Role = Role.Administrator
                },
                new User
                {
                    Id = 3,
                    FirstName = "Alice",
                    LastName = "Brown",
                    Email = "alice.brown@example.com",
                    PasswordHash = "hashedpassword3",
                    Role = Role.Customer
                }
            );

            // Seeding Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    MovieId = 1,
                    UserId = 1,
                    Comment = "Amazing action sequences!",
                    Rating = 5,
                    ReviewDate = new DateTime(2024, 12, 2, 14, 30, 0)
                },
                new Review
                {
                    Id = 2,
                    MovieId = 2,
                    UserId = 2,
                    Comment = "Hilarious from start to finish.",
                    Rating = 4,
                    ReviewDate = new DateTime(2024, 12, 2, 14, 30, 0)
                }
            );

            modelBuilder.Entity<Discount>().HasData(
                new Discount
                {
                    Id = 1,
                    Title = "Holiday Special",
                    Description = "10% off for the holiday season",
                    DiscountPercentage = 10m,
                    StartDate = new DateTimeOffset(new DateTime(2024, 12, 15)),
                    EndDate = new DateTimeOffset(new DateTime(2025, 1, 5)),
                    Code = null, // General discount
                    IsUnlimited = true
                },
                new Discount
                {
                    Id = 2,
                    Title = "New User Promo",
                    Description = "15% off for new users",
                    DiscountPercentage = 15m,
                    StartDate = new DateTimeOffset(new DateTime(2024, 11, 1)),
                    EndDate = new DateTimeOffset(new DateTime(2024, 12, 31)),
                    Code = "NEWUSER2024",
                    IsUnlimited = false,
                    IsUsed = false
                },
                new Discount
                {
                    Id = 3,
                    Title = "Black Friday Deal",
                    Description = "50% off for Black Friday",
                    DiscountPercentage = 50m,
                    StartDate = new DateTimeOffset(new DateTime(2024, 11, 29)),
                    EndDate = new DateTimeOffset(new DateTime(2024, 11, 30)),
                    Code = "BLACKFRIDAY",
                    IsUnlimited = false,
                    IsUsed = false
                }
            );

            // Seeding Payment Details
            modelBuilder.Entity<PaymentDetail>().HasData(
                new PaymentDetail
                {
                    Id = 1,
                    Amount = 25.00m,
                    Method = PaymentMethod.Online,
                    Date = new DateTime(2024, 12, 1, 19, 0, 0),
                    BookingId = 1
                }
            );

            // Seeding Bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    UserId = 1,
                    NumberOfTickets = 2,
                    MovieSessionId = 1,
                    PaymentDetailId = 1 
                }
            );


            modelBuilder.Entity("BookingSeat").HasData(
                new { BookingId = 1, SeatId = 1 },
                new { BookingId = 1, SeatId = 2 }  
            );

        }
    }
}
