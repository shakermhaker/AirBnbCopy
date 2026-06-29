using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Örn: "Yaz Tatili Rotası"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<UserList> UserLists { get; set; } = new List<UserList>();
    }

}
