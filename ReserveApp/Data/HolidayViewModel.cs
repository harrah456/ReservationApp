namespace ReserveApp.Data
{
    public class HolidaySaveModel
    {
        public Hotel Hotel { get; set; }
        public Tour Tour { get; set; }
    }

    public class HolidaySearchModel
    {
        public IEnumerable<Hotel> Hotels { get; set; }

        public IEnumerable<Tour> Tours { get; set; }
    }
}
