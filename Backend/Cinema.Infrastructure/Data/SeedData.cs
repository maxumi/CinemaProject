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
                new CinemaHall { Id = 2, Name = "Hall B", Capacity = 100 },
                new CinemaHall { Id = 3, Name = "Hall C", Capacity = 150 },
                new CinemaHall { Id = 4, Name = "Hall D", Capacity = 100 }
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
                    Title = "Epic Adventure",
                    DurationMinutes = 130,
                    ReleaseDate = new DateTime(2024, 1, 15),
                    Description = "A thrilling journey through uncharted lands."
                },
                new Movie
                {
                    Id = 2,
                    Title = "Comedy Nights",
                    DurationMinutes = 95,
                    ReleaseDate = new DateTime(2023, 10, 1),
                    Description = "A rib-tickling comedy that'll leave you in splits."
                },
                new Movie
                {
                    Id = 3,
                    Title = "Sci-Fi Wonders",
                    DurationMinutes = 120,
                    ReleaseDate = new DateTime(2024, 5, 20),
                    Description = "Explore the wonders of the universe."
                },
                new Movie
                {
                    Id = 4,
                    Title = "Historical Drama",
                    DurationMinutes = 145,
                    ReleaseDate = new DateTime(2023, 8, 25),
                    Description = "A moving story set in ancient times."
                },
                new Movie
                {
                    Id = 5,
                    Title = "Mystery Unfolded",
                    DurationMinutes = 110,
                    ReleaseDate = new DateTime(2023, 12, 10),
                    Description = "A detective unravels a mind-bending mystery."
                },
                new Movie
                {
                    Id = 6,
                    Title = "Romantic Bliss",
                    DurationMinutes = 105,
                    ReleaseDate = new DateTime(2024, 2, 14),
                    Description = "A heartwarming tale of love and destiny."
                }
            );

            // Seeding MovieSessions
            modelBuilder.Entity<MovieSession>().HasData(
                new MovieSession
                {
                    Id = 1,
                    MovieId = 1,
                    CinemaHallId = 1,
                    StartTime = new DateTime(2024, 12, 1, 10, 0, 0),
                    EndTime = new DateTime(2024, 12, 1, 12, 30, 0),
                    Price = 15.00m
                },
                new MovieSession
                {
                    Id = 2,
                    MovieId = 1,
                    CinemaHallId = 2,
                    StartTime = new DateTime(2024, 12, 1, 14, 0, 0),
                    EndTime = new DateTime(2024, 12, 1, 16, 30, 0),
                    Price = 15.00m
                },
                new MovieSession
                {
                    Id = 3,
                    MovieId = 1,
                    CinemaHallId = 3,
                    StartTime = new DateTime(2024, 12, 1, 18, 0, 0),
                    EndTime = new DateTime(2024, 12, 1, 20, 30, 0),
                    Price = 15.00m
                },
                new MovieSession
                {
                    Id = 4,
                    MovieId = 1,
                    CinemaHallId = 4,
                    StartTime = new DateTime(2024, 12, 1, 21, 0, 0),
                    EndTime = new DateTime(2024, 12, 1, 23, 30, 0),
                    Price = 15.00m
                },
                new MovieSession
                {
                    Id = 5,
                    MovieId = 2,
                    CinemaHallId = 1,
                    StartTime = new DateTime(2024, 12, 2, 10, 0, 0),
                    EndTime = new DateTime(2024, 12, 2, 11, 35, 0),
                    Price = 12.00m
                },
                new MovieSession
                {
                    Id = 6,
                    MovieId = 3,
                    CinemaHallId = 2,
                    StartTime = new DateTime(2024, 12, 3, 12, 0, 0),
                    EndTime = new DateTime(2024, 12, 3, 14, 0, 0),
                    Price = 14.00m
                },
                new MovieSession
                {
                    Id = 7,
                    MovieId = 4,
                    CinemaHallId = 3,
                    StartTime = new DateTime(2024, 12, 4, 15, 0, 0),
                    EndTime = new DateTime(2024, 12, 4, 17, 25, 0),
                    Price = 16.00m
                },
                new MovieSession
                {
                    Id = 8,
                    MovieId = 5,
                    CinemaHallId = 4,
                    StartTime = new DateTime(2024, 12, 5, 18, 0, 0),
                    EndTime = new DateTime(2024, 12, 5, 19, 50, 0),
                    Price = 13.00m
                },
                new MovieSession
                {
                    Id = 9,
                    MovieId = 6,
                    CinemaHallId = 1,
                    StartTime = new DateTime(2024, 12, 6, 20, 0, 0),
                    EndTime = new DateTime(2024, 12, 6, 21, 45, 0),
                    Price = 11.00m
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
                    PasswordHash = "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.",
                    Role = Role.Customer
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PasswordHash = "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.",
                    Role = Role.Administrator
                },
                new User
                {
                    Id = 3,
                    FirstName = "Alice",
                    LastName = "Brown",
                    Email = "alice.brown@example.com",
                    PasswordHash = "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.",
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
