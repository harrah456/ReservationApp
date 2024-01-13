using ReserveApp.Data;

namespace ReserveApp.Services
{
    public interface IReservationService
    {
        Task<bool> CreateReservation(Reservation reservation);

    }
}
