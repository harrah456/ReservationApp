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

        public async Task<List<Hotel>> SearchHotelsByDateRange(DateTime startDate, DateTime endDate)
        {
            var hotels = await _context.Hotels.Include(h => h.Reservations)
        .Where(h => !h.Reservations.Any(r =>
            (r.ReservationStartDate <= startDate && r.ReservationEndDate >= startDate) ||
            (r.ReservationStartDate <= endDate && r.ReservationEndDate >= endDate)))
        .ToListAsync();

            foreach (var hotel in hotels)
            {
                hotel.AvailableRooms = CalculateAvailableRooms(hotel, startDate, endDate);
            }

            return hotels;
        }

        private Dictionary<RoomType, int> CalculateAvailableRooms(Hotel hotel, DateTime startDate, DateTime endDate)
        {
            var reservedRooms = hotel.Reservations
                .Where(r => (r.ReservationStartDate <= startDate && r.ReservationEndDate >= startDate) ||
                            (r.ReservationStartDate <= endDate && r.ReservationEndDate >= endDate))
                .GroupBy(r => r.RoomType)
                .ToDictionary(g => g.Key, g => g.Sum(r => r.NumberOfAdults + r.NumberOfChildren));

            var totalRooms = 20; // Assuming 20 spaces available for each room type

            var availableRooms = new Dictionary<RoomType, int>
            {
                { RoomType.Single, Math.Max(0, totalRooms - reservedRooms.GetValueOrDefault(RoomType.Single)) },
                { RoomType.Double, Math.Max(0, totalRooms - reservedRooms.GetValueOrDefault(RoomType.Double)) },
                { RoomType.FamilySuite, Math.Max(0, totalRooms - reservedRooms.GetValueOrDefault(RoomType.FamilySuite)) }
            };

            return availableRooms;
        }
    }
}
