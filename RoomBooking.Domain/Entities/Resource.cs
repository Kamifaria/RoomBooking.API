using System;

namespace RoomBooking.Domain.Entities
{
    public class Resource : BaseEntity
    {
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string DiskSize { get; set; }
        public string HDD { get; set; }
        public string SSD { get; set; }
        public string VideoCardSize { get; set; }
        public string VideoCard { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
