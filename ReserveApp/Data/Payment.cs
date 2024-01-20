using System.ComponentModel.DataAnnotations;

namespace ReserveApp.Data
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
