using ReserveApp.Data;

namespace ReserveApp.Services
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetAllHotels();
        Task<List<Hotel>> SearchHotelsByDateRange(DateTime startDate, DateTime endDate);
        Task<bool> HotelAvailableByDateRange(Hotel hotel, DateTime startDate, DateTime endDate);
    }
}
