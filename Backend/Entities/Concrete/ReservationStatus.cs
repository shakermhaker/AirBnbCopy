using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ReservationStatus : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Örn: Pending, Paid, Confirmed, Cancelled
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
