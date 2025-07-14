using System.Collections.Generic;

namespace RoomBooking.Domain.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public int Seats { get; set; }
        public IList<Reservation> Reservations { get; set; }
    }
}
