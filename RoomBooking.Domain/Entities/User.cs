using System.Collections.Generic;

namespace RoomBooking.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<Resource> Resources { get; set; }
        public IList<Reservation> Reservations { get; set; }
    }
}
