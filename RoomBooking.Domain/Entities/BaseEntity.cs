using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
