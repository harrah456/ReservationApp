using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReserveApp.Data
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        public string Name { get; set; }
        public decimal SingleBedPrice { get; set; }
        public decimal DoubleBedPrice { get; set; }
        public decimal FamilySuitePrice { get; set; }
        //public int SingleBedCapacity { get; set; } = 20;
        //public int DoubleBedCapacity { get; set; } = 20;
        //public int FamilySuiteCapacity { get; set; } = 20;
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        // Add the AvailableRooms property
        [NotMapped]
        public Dictionary<RoomType, int> AvailableRooms { get; set; } = new Dictionary<RoomType, int> { { RoomType.Single, 20 }, { RoomType.Double, 20 }, { RoomType.FamilySuite, 20 } };

    }
}
