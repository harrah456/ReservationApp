using ReserveApp.Data;

namespace ReserveApp.Services
{
    public interface IReservatonService
    {
        Task<bool> CreateReservation(Reservation reservation);
        public Task<List<Reservation>> GetReservationsByCustomerId(string customerId);
        public Task<Reservation> GetReservationById(int reservationId);

        public Task UpdateDepositAmount(int reservationId, decimal amount);

        public Task<bool> CancelReservation(Reservation reservation);
        public Task<bool> EditReservation(Reservation reservation, decimal initialDepositAmount);

    }
}
