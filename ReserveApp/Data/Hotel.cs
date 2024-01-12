using System.ComponentModel.DataAnnotations;

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
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
