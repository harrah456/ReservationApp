using ReserveApp.Data;

namespace ReserveApp.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateReservation(Reservation reservation)
        {
            try
            {
                // Add the reservation to the database
                _dbContext.Reservations.Add(reservation);
                await _dbContext.SaveChangesAsync();

                return true; // Reservation created successfully
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine(ex.ToString());
                return false; // Failed to create reservation
            }
        }
    }
}
