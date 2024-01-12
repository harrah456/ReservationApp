using Microsoft.EntityFrameworkCore;
using ReserveApp.Data;

namespace ReserveApp.Services
{
    public class HotelService
    {
        private ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<List<Hotel>> SearchHotelsByDateRange(DateTime? startDate, DateTime? endDate)
        {
            var hotels = await _context.Hotels
                .Include(h => h.Reservations)
                .Where(h => !h.Reservations.Any(r => r.ReservationDate >= startDate && r.ReservationDate <= endDate))
                .ToListAsync();

            return hotels;
        }
    }
}
