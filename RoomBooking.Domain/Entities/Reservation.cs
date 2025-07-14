using System;
using System.Collections.Generic;

namespace RoomBooking.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public string Name { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }

        public int EventCategoryId { get; set; }
        public EventCategory EventCategory { get; set; }

        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }

        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public IList<User> Participants { get; set; } = new List<User>();
    }
}
