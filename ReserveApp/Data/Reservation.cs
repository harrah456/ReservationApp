using System.ComponentModel.DataAnnotations;

namespace ReserveApp.Data
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public string CustomerId { get; set; } // Reference to ApplicationUser.Id
        public int HotelId { get; set; }
        public int TourId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime ConfirmationDate { get; set; }
    }
}
