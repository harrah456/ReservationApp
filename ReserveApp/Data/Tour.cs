using System.ComponentModel.DataAnnotations;

namespace ReserveApp.Data
{
    public class Tour
    {
        [Key]
        public int TourId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int AvailableSpaces { get; set; }

    }
}
