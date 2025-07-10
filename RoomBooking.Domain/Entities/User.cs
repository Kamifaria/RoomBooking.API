namespace RoomBooking.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public Login Login { get; set; }

        public IList<RoomReservation> Reservations { get; set; }
        public IList<Resource> Resources { get; set; }
    }
}
