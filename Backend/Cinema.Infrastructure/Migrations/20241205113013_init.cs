using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CinemaHalls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHalls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUnlimited = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinemaHallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_CinemaHalls_CinemaHallId",
                        column: x => x.CinemaHallId,
                        principalTable: "CinemaHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenre",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenre", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieGenre_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CinemaHallId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieSessions_CinemaHalls_CinemaHallId",
                        column: x => x.CinemaHallId,
                        principalTable: "CinemaHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieSessions_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NumberOfTickets = table.Column<int>(type: "int", nullable: false),
                    MovieSessionId = table.Column<int>(type: "int", nullable: false),
                    PaymentDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_MovieSessions_MovieSessionId",
                        column: x => x.MovieSessionId,
                        principalTable: "MovieSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingSeat",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSeat", x => new { x.BookingId, x.SeatId });
                    table.ForeignKey(
                        name: "FK_BookingSeat_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingSeat_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CinemaHalls",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, 150, "Hall A" },
                    { 2, 100, "Hall B" },
                    { 3, 150, "Hall C" },
                    { 4, 100, "Hall D" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "Code", "Description", "DiscountPercentage", "EndDate", "IsUnlimited", "IsUsed", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, null, "10% off for the holiday season", 10m, new DateTimeOffset(new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), true, false, new DateTimeOffset(new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Holiday Special" },
                    { 2, "NEWUSER2024", "15% off for new users", 15m, new DateTimeOffset(new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), false, false, new DateTimeOffset(new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "New User Promo" },
                    { 3, "BLACKFRIDAY", "50% off for Black Friday", 50m, new DateTimeOffset(new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), false, false, new DateTimeOffset(new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Black Friday Deal" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "DurationMinutes", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "A thrilling journey through uncharted lands.", 130, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Epic Adventure" },
                    { 2, "A rib-tickling comedy that'll leave you in splits.", 95, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comedy Nights" },
                    { 3, "Explore the wonders of the universe.", 120, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sci-Fi Wonders" },
                    { 4, "A moving story set in ancient times.", 145, new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historical Drama" },
                    { 5, "A detective unravels a mind-bending mystery.", 110, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mystery Unfolded" },
                    { 6, "A heartwarming tale of love and destiny.", 105, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romantic Bliss" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", "$2a$11$JTCywpQ32wbqj41LAOL3u.hCYrpGPtWF9WML4A.lMWpmPZCKFSS..", 0 },
                    { 2, "jane.smith@example.com", "Jane", "Smith", "$2a$11$JTCywpQ32wbqj41LAOL3u.hCYrpGPtWF9WML4A.lMWpmPZCKFSS..", 1 },
                    { 3, "alice.brown@example.com", "Alice", "Brown", "$2a$11$JTCywpQ32wbqj41LAOL3u.hCYrpGPtWF9WML4A.lMWpmPZCKFSS..", 0 }
                });

            migrationBuilder.InsertData(
                table: "MovieSessions",
                columns: new[] { "Id", "CinemaHallId", "EndTime", "MovieId", "Price", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 1, 12, 30, 0, 0, DateTimeKind.Unspecified), 1, 15.00m, new DateTime(2024, 12, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2024, 12, 1, 16, 30, 0, 0, DateTimeKind.Unspecified), 1, 15.00m, new DateTime(2024, 12, 1, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2024, 12, 1, 20, 30, 0, 0, DateTimeKind.Unspecified), 1, 15.00m, new DateTime(2024, 12, 1, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, new DateTime(2024, 12, 1, 23, 30, 0, 0, DateTimeKind.Unspecified), 1, 15.00m, new DateTime(2024, 12, 1, 21, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, new DateTime(2024, 12, 2, 11, 35, 0, 0, DateTimeKind.Unspecified), 2, 12.00m, new DateTime(2024, 12, 2, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, new DateTime(2024, 12, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), 3, 14.00m, new DateTime(2024, 12, 3, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 3, new DateTime(2024, 12, 4, 17, 25, 0, 0, DateTimeKind.Unspecified), 4, 16.00m, new DateTime(2024, 12, 4, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 4, new DateTime(2024, 12, 5, 19, 50, 0, 0, DateTimeKind.Unspecified), 5, 13.00m, new DateTime(2024, 12, 5, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, new DateTime(2024, 12, 6, 21, 45, 0, 0, DateTimeKind.Unspecified), 6, 11.00m, new DateTime(2024, 12, 6, 20, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "MovieId", "Rating", "ReviewDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Amazing action sequences!", 1, 5, new DateTime(2024, 12, 2, 14, 30, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Hilarious from start to finish.", 2, 4, new DateTime(2024, 12, 2, 14, 30, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "CinemaHallId", "SeatNumber" },
                values: new object[,]
                {
                    { 1, 1, "A1" },
                    { 2, 1, "A2" },
                    { 3, 2, "B1" },
                    { 4, 2, "B2" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "MovieSessionId", "NumberOfTickets", "PaymentDetailId", "UserId" },
                values: new object[] { 1, 1, 2, 1, 1 });

            migrationBuilder.InsertData(
                table: "BookingSeat",
                columns: new[] { "BookingId", "SeatId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "PaymentDetails",
                columns: new[] { "Id", "Amount", "BookingId", "Date", "Method" },
                values: new object[] { 1, 25.00m, 1, new DateTime(2024, 12, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MovieSessionId",
                table: "Bookings",
                column: "MovieSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeat_SeatId",
                table: "BookingSeat",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_GenreId",
                table: "MovieGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieSessions_CinemaHallId",
                table: "MovieSessions",
                column: "CinemaHallId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieSessions_MovieId",
                table: "MovieSessions",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_BookingId",
                table: "PaymentDetails",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_CinemaHallId",
                table: "Seats",
                column: "CinemaHallId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingSeat");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "MovieGenre");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "MovieSessions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CinemaHalls");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
