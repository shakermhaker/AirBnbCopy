using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }

        public int RentalHouseId { get; set; }
        public RentalHouse RentalHouse { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PaidPrice { get; set; } // Rezervasyon anındaki net fiyat fikslemesi

        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; } = null!;
    }
}
