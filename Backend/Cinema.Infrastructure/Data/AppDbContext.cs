using Cinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP18;Database=CinemaDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many Configuration for Reservation and Seat
            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Seats)
                .WithMany(s => s.Reservations)
                .UsingEntity(j => j.ToTable("ReservationSeats")); // Table Name

            // Configure One-to-One Relationship for PaymentDetail
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.PaymentDetail)
                .WithOne(p => p.Reservation)
                .HasForeignKey<PaymentDetail>(p => p.ReservationId);
        }
    }
}