﻿// <auto-generated />
using System;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250113124017_updatedSeeData")]
    partial class updatedSeeData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookingSeat", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.HasKey("BookingId", "SeatId");

                    b.HasIndex("SeatId");

                    b.ToTable("BookingSeat");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            SeatId = 1
                        },
                        new
                        {
                            BookingId = 1,
                            SeatId = 2
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieSessionId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfTickets")
                        .HasColumnType("int");

                    b.Property<int>("PaymentDetailId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieSessionId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MovieSessionId = 1,
                            NumberOfTickets = 2,
                            PaymentDetailId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.CinemaHall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CinemaHalls");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 150,
                            Name = "Hall A"
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 100,
                            Name = "Hall B"
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 150,
                            Name = "Hall C"
                        },
                        new
                        {
                            Id = 4,
                            Capacity = 100,
                            Name = "Hall D"
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(5,2)");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsUnlimited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "10% off for the holiday season",
                            DiscountPercentage = 10m,
                            EndDate = new DateTimeOffset(new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsUnlimited = true,
                            IsUsed = false,
                            StartDate = new DateTimeOffset(new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Title = "Holiday Special"
                        },
                        new
                        {
                            Id = 2,
                            Code = "NEWUSER2024",
                            Description = "15% off for new users",
                            DiscountPercentage = 15m,
                            EndDate = new DateTimeOffset(new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsUnlimited = false,
                            IsUsed = false,
                            StartDate = new DateTimeOffset(new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Title = "New User Promo"
                        },
                        new
                        {
                            Id = 3,
                            Code = "BLACKFRIDAY",
                            Description = "50% off for Black Friday",
                            DiscountPercentage = 50m,
                            EndDate = new DateTimeOffset(new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsUnlimited = false,
                            IsUsed = false,
                            StartDate = new DateTimeOffset(new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Title = "Black Friday Deal"
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Drama"
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A thrilling journey through uncharted lands.",
                            DurationMinutes = 130,
                            ReleaseDate = new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Epic Adventure"
                        },
                        new
                        {
                            Id = 2,
                            Description = "A rib-tickling comedy that'll leave you in splits.",
                            DurationMinutes = 95,
                            ReleaseDate = new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Comedy Nights"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Explore the wonders of the universe.",
                            DurationMinutes = 120,
                            ReleaseDate = new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sci-Fi Wonders"
                        },
                        new
                        {
                            Id = 4,
                            Description = "A moving story set in ancient times.",
                            DurationMinutes = 145,
                            ReleaseDate = new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Historical Drama"
                        },
                        new
                        {
                            Id = 5,
                            Description = "A detective unravels a mind-bending mystery.",
                            DurationMinutes = 110,
                            ReleaseDate = new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Mystery Unfolded"
                        },
                        new
                        {
                            Id = 6,
                            Description = "A heartwarming tale of love and destiny.",
                            DurationMinutes = 105,
                            ReleaseDate = new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Romantic Bliss"
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.MovieSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CinemaHallId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CinemaHallId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieSessions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CinemaHallId = 1,
                            EndTime = new DateTime(2024, 12, 1, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 1,
                            Price = 15.00m,
                            StartTime = new DateTime(2024, 12, 1, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CinemaHallId = 2,
                            EndTime = new DateTime(2024, 12, 1, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 1,
                            Price = 15.00m,
                            StartTime = new DateTime(2024, 12, 1, 14, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CinemaHallId = 3,
                            EndTime = new DateTime(2024, 12, 1, 20, 30, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 1,
                            Price = 15.00m,
                            StartTime = new DateTime(2024, 12, 1, 18, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CinemaHallId = 4,
                            EndTime = new DateTime(2024, 12, 1, 23, 30, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 1,
                            Price = 15.00m,
                            StartTime = new DateTime(2024, 12, 1, 21, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CinemaHallId = 1,
                            EndTime = new DateTime(2024, 12, 2, 11, 35, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 2,
                            Price = 12.00m,
                            StartTime = new DateTime(2024, 12, 2, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CinemaHallId = 2,
                            EndTime = new DateTime(2024, 12, 3, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 3,
                            Price = 14.00m,
                            StartTime = new DateTime(2024, 12, 3, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            CinemaHallId = 3,
                            EndTime = new DateTime(2024, 12, 4, 17, 25, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 4,
                            Price = 16.00m,
                            StartTime = new DateTime(2024, 12, 4, 15, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            CinemaHallId = 4,
                            EndTime = new DateTime(2024, 12, 5, 19, 50, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 5,
                            Price = 13.00m,
                            StartTime = new DateTime(2024, 12, 5, 18, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            CinemaHallId = 1,
                            EndTime = new DateTime(2024, 12, 6, 21, 45, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 6,
                            Price = 11.00m,
                            StartTime = new DateTime(2024, 12, 6, 20, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.PaymentDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Method")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("PaymentDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 25.00m,
                            BookingId = 1,
                            Date = new DateTime(2024, 12, 1, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            Method = 0
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Comment = "Amazing action sequences!",
                            MovieId = 1,
                            Rating = 5,
                            ReviewDate = new DateTime(2024, 12, 2, 14, 30, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Comment = "Hilarious from start to finish.",
                            MovieId = 2,
                            Rating = 4,
                            ReviewDate = new DateTime(2024, 12, 2, 14, 30, 0, 0, DateTimeKind.Unspecified),
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CinemaHallId")
                        .HasColumnType("int");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CinemaHallId");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CinemaHallId = 1,
                            SeatNumber = "A1"
                        },
                        new
                        {
                            Id = 2,
                            CinemaHallId = 1,
                            SeatNumber = "A2"
                        },
                        new
                        {
                            Id = 3,
                            CinemaHallId = 1,
                            SeatNumber = "A3"
                        },
                        new
                        {
                            Id = 4,
                            CinemaHallId = 1,
                            SeatNumber = "A4"
                        },
                        new
                        {
                            Id = 5,
                            CinemaHallId = 1,
                            SeatNumber = "A5"
                        },
                        new
                        {
                            Id = 6,
                            CinemaHallId = 1,
                            SeatNumber = "A6"
                        },
                        new
                        {
                            Id = 7,
                            CinemaHallId = 2,
                            SeatNumber = "B1"
                        },
                        new
                        {
                            Id = 8,
                            CinemaHallId = 2,
                            SeatNumber = "B2"
                        },
                        new
                        {
                            Id = 9,
                            CinemaHallId = 2,
                            SeatNumber = "B3"
                        },
                        new
                        {
                            Id = 10,
                            CinemaHallId = 2,
                            SeatNumber = "B4"
                        },
                        new
                        {
                            Id = 11,
                            CinemaHallId = 2,
                            SeatNumber = "B5"
                        },
                        new
                        {
                            Id = 12,
                            CinemaHallId = 3,
                            SeatNumber = "C1"
                        },
                        new
                        {
                            Id = 13,
                            CinemaHallId = 3,
                            SeatNumber = "C2"
                        },
                        new
                        {
                            Id = 14,
                            CinemaHallId = 3,
                            SeatNumber = "C3"
                        },
                        new
                        {
                            Id = 15,
                            CinemaHallId = 3,
                            SeatNumber = "C4"
                        },
                        new
                        {
                            Id = 16,
                            CinemaHallId = 3,
                            SeatNumber = "C5"
                        },
                        new
                        {
                            Id = 17,
                            CinemaHallId = 3,
                            SeatNumber = "C6"
                        },
                        new
                        {
                            Id = 18,
                            CinemaHallId = 3,
                            SeatNumber = "C7"
                        },
                        new
                        {
                            Id = 19,
                            CinemaHallId = 4,
                            SeatNumber = "D1"
                        },
                        new
                        {
                            Id = 20,
                            CinemaHallId = 4,
                            SeatNumber = "D2"
                        },
                        new
                        {
                            Id = 21,
                            CinemaHallId = 4,
                            SeatNumber = "D3"
                        },
                        new
                        {
                            Id = 22,
                            CinemaHallId = 4,
                            SeatNumber = "D4"
                        },
                        new
                        {
                            Id = 23,
                            CinemaHallId = 4,
                            SeatNumber = "D5"
                        },
                        new
                        {
                            Id = 24,
                            CinemaHallId = 4,
                            SeatNumber = "D6"
                        });
                });

            modelBuilder.Entity("Cinema.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PasswordHash = "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            PasswordHash = "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.",
                            Role = 1
                        },
                        new
                        {
                            Id = 3,
                            Email = "alice.brown@example.com",
                            FirstName = "Alice",
                            LastName = "Brown",
                            PasswordHash = "$2a$11$muZFY1XLH0Ks/ASxO6mg7uU1DyyXuRBNHKhWW/10yY.FJOZ3nRwC.",
                            Role = 0
                        });
                });

            modelBuilder.Entity("MovieGenre", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("BookingSeat", b =>
                {
                    b.HasOne("Cinema.Domain.Entities.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinema.Domain.Entities.Seat", null)
                        .WithMany()
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Booking", b =>
                {
                    b.HasOne("Cinema.Domain.Entities.MovieSession", "MovieSession")
                        .WithMany("Bookings")
                        .HasForeignKey("MovieSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinema.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieSession");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cinema.Domain.Entities.MovieSession", b =>
                {
                    b.HasOne("Cinema.Domain.Entities.CinemaHall", "CinemaHall")
                        .WithMany("MovieSessions")
                        .HasForeignKey("CinemaHallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinema.Domain.Entities.Movie", "Movie")
                        .WithMany("MovieSessions")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CinemaHall");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Cinema.Domain.Entities.PaymentDetail", b =>
                {
                    b.HasOne("Cinema.Domain.Entities.Booking", "Booking")
                        .WithOne("PaymentDetail")
                        .HasForeignKey("Cinema.Domain.Entities.PaymentDetail", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Review", b =>
                {
                    b.HasOne("Cinema.Domain.Entities.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinema.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Seat", b =>
                {
                    b.HasOne("Cinema.Domain.Entities.CinemaHall", "CinemaHall")
                        .WithMany("Seats")
                        .HasForeignKey("CinemaHallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CinemaHall");
                });

            modelBuilder.Entity("MovieGenre", b =>
                {
                    b.HasOne("Cinema.Domain.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Cinema.Domain.Entities.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Booking", b =>
                {
                    b.Navigation("PaymentDetail")
                        .IsRequired();
                });

            modelBuilder.Entity("Cinema.Domain.Entities.CinemaHall", b =>
                {
                    b.Navigation("MovieSessions");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("Cinema.Domain.Entities.Movie", b =>
                {
                    b.Navigation("MovieSessions");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Cinema.Domain.Entities.MovieSession", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
