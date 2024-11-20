using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Entities
{
    public enum PaymentMethod
    {
        Online,
        OnPremise
    }

    public class PaymentDetail
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime Date { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
