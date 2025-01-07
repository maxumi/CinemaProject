using Microsoft.EntityFrameworkCore;
using Cinema.Domain.Entities;

namespace Cinema.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public required DbSet<User> Users { get; set; }
        public required DbSet<Movie> Movies { get; set; }
        public required DbSet<Genre> Genres { get; set; }
        public required DbSet<MovieSession> MovieSessions { get; set; }
        public required DbSet<CinemaHall> CinemaHalls { get; set; }
        public required DbSet<Booking> Bookings { get; set; }
        public required DbSet<Seat> Seats { get; set; }
        public required DbSet<PaymentDetail> PaymentDetails { get; set; }
        public required DbSet<Review> Reviews { get; set; }
        public required DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many Configuration for Movie and Genre
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieGenre", // Explicit name for the join table
                    j => j.HasOne<Genre>()
                          .WithMany()
                          .HasForeignKey("GenreId")
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Movie>()
                          .WithMany()
                          .HasForeignKey("MovieId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasKey("MovieId", "GenreId") // Define composite key
                );


            // Many-to-Many Configuration for Reservation and Seat
            modelBuilder.Entity<Booking>()
                .HasMany(r => r.Seats)
                .WithMany(s => s.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "BookingSeat", // Name of the implicit join table
                    r => r.HasOne<Seat>()
                          .WithMany()
                          .HasForeignKey("SeatId")
                          .OnDelete(DeleteBehavior.Restrict), // Prevent cascading delete on Seats
                    s => s.HasOne<Booking>()
                          .WithMany()
                          .HasForeignKey("BookingId")
                          .OnDelete(DeleteBehavior.Cascade) // Prevent cascading delete on Reservations
                );



            // Configure One-to-One Relationship for PaymentDetail
            modelBuilder.Entity<Booking>()
                .HasOne(r => r.PaymentDetail)
                .WithOne(p => p.Booking)
                .HasForeignKey<PaymentDetail>(p => p.BookingId);

            // Decimal Precision Configuration
            modelBuilder.Entity<Discount>()
                .Property(d => d.DiscountPercentage)
                .HasColumnType("decimal(5,2)"); // Discount percentages (e.g., 99.99)

            modelBuilder.Entity<MovieSession>()
                .Property(ms => ms.Price)
                .HasColumnType("decimal(18,2)"); // Movie session price (currency format)

            modelBuilder.Entity<PaymentDetail>()
                .Property(pd => pd.Amount)
                .HasColumnType("decimal(18,2)"); // Payment amount (currency format)


            modelBuilder.Seed(); // Seed data with premade ones
        }
    }
}