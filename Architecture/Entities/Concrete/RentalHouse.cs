using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class RentalHouse : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }

        // Foreign Key & Navigation Property (Ev hangi ilçede?)
        public int DistrictId { get; set; }
        public District District { get; set; } = null!;

        // Navigation Property
        // Evin dahil olduğu kullanıcı ilişkileri (Örn: Favoriler, Geçmiş Rezervasyonlar veya İlan Sahipleri)
        public int OwnerUserId { get; set; }
        public User OwnerUser { get; set; } = null!;
        public ICollection<Reservation> Reservations{ get; set; } = new List<Reservation>();
        public ICollection<RentalHouseDatePrice> DatePrices { get; set; } = new List<RentalHouseDatePrice>();


    }
}
