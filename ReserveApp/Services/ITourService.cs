using ReserveApp.Data;

namespace ReserveApp.Services
{
    public interface ITourService
    {
        Task<List<Tour>> GetAllTours();
        Task<List<Tour>> SearchToursByDateRange(DateTime startDate, DateTime endDate);
        Task<bool> TourAvailableByDateRange(Tour Tour, DateTime startDate, DateTime endDate);
    }
}
