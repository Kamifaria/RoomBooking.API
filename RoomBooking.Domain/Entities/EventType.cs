using System.Collections.Generic;

namespace RoomBooking.Domain.Entities
{
    public class EventType : BaseEntity
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
