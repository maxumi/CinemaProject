﻿using System.Collections.Generic;

namespace Cinema.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int NumberOfTickets { get; set; }
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public string PaymentDetailId { get; set; }
        public PaymentDetail PaymentDetail { get; set; }

        // Many-to-Many Relationship with Seats
        public ICollection<Seat> Seats { get; set; }
    }
}