using Microsoft.EntityFrameworkCore;
using ReserveApp.Data;

namespace ReserveApp.Services
{
    public class TourService : ITourService
    {
        private readonly ApplicationDbContext _context;

        public TourService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tour>> GetAllTours()
        {
            return await _context.Tours.ToListAsync();
        }

        public async Task<List<Tour>> SearchToursByDateRange(DateTime startDate, DateTime endDate)
        {
            var tours = await _context.Tours.Include(h => h.Reservations)
        .Where(h => !h.Reservations.Any(r =>
            (r.ReservationStartDate <= startDate && r.ReservationEndDate >= startDate) ||
            (r.ReservationStartDate <= endDate && r.ReservationEndDate >= endDate)))
        .ToListAsync();

            return tours;
        }

        public async Task<bool> TourAvailableByDateRange(Tour tour, DateTime startDate, DateTime endDate)
        {
            var available = await _context.Tours
                .AnyAsync(h => h.TourId == tour.TourId && !h.Reservations.Any(r =>
                (r.ReservationStartDate <= startDate && r.ReservationEndDate >= startDate) ||
                (r.ReservationStartDate <= endDate && r.ReservationEndDate >= endDate)));
            return available;
        }
    }
}
