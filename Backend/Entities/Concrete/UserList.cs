using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class UserList : IEntity
    {
        public int Id { get; set; }

        // Listeyi kullanan/paylaşılan kullanıcı
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // İlgili liste
        public int ListId { get; set; }
        public List List { get; set; } = null!;

        // Listenin sahibi olan kullanıcı (Diyagramdaki OwnerId)
        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;
    }
}
