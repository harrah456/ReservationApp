using Microsoft.EntityFrameworkCore;
using ReserveApp.Data;

namespace ReserveApp.Services
{
    public class ReservationService : IReservatonService
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

                if (reservation.DepositAmount > 0)
                {
                    Payment payment = new Payment
                    {
                        ReservationId = reservation.ReservationId,
                        Amount = reservation.DepositAmount,
                        PaymentDate = DateTime.Now
                    };
                    await _dbContext.Payments.AddAsync(payment);
                    await _dbContext.SaveChangesAsync();
                }

                return true; // Reservation created successfully
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine(ex.ToString());
                return false; // Failed to create reservation
            }
        }
        
        public async Task<bool> EditReservation(Reservation reservation, decimal initialDepositAmount)
        {
            try
            {
                // Add the reservation to the database
                _dbContext.Reservations.Update(reservation);
                await _dbContext.SaveChangesAsync();

                decimal newDepositAmount = 0m;

                if (initialDepositAmount < reservation.DepositAmount)
                {
                    newDepositAmount = reservation.DepositAmount - initialDepositAmount;
                }
                else if (initialDepositAmount > reservation.DepositAmount)
                {
                    // delete previous all deposits and insert new one
                    await _dbContext.Payments.Where(p => p.ReservationId == reservation.ReservationId).ExecuteDeleteAsync();
                    await _dbContext.SaveChangesAsync();

                    newDepositAmount = initialDepositAmount - reservation.DepositAmount;
                }

                if (reservation.DepositAmount > 0)
                {
                    Payment payment = new Payment
                    {
                        ReservationId = reservation.ReservationId,
                        Amount = newDepositAmount,
                        PaymentDate = DateTime.Now
                    };
                    await _dbContext.Payments.AddAsync(payment);
                    await _dbContext.SaveChangesAsync();
                }

                return true; // Reservation created successfully
            }
            catch (Exception ex)
            {
                return false; // Failed to create reservation
            }
        }

        public async Task UpdateDepositAmount(int reservationId, decimal amount)
        {
            var reservation = await _dbContext.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                reservation.DepositAmount += amount;
                if (reservation.DepositAmount >= reservation.TotalAmount * 0.2m)
                {
                    reservation.IsConfirmed = true;
                    reservation.ConfirmationDate = DateTime.Now;
                }
                _dbContext.Reservations.Update(reservation);
                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<bool> CancelReservation(Reservation reservation)
        {
            bool cancelled = false;

            if (reservation != null)
            {
                reservation.Status = false;
                _dbContext.Reservations.Update(reservation);
                var updatedRows = await _dbContext.SaveChangesAsync();

                cancelled = updatedRows > 0;
            }
            return cancelled;
        }

        public async Task<bool> ModifyReservation(Reservation reservation)
        {
            // Check if modification is within the allowed window
            if (reservation.ReservationStartDate.AddDays(-14) >= DateTime.Now)
            {
                // Calculate surcharge if needed
                var surcharge = reservation.TotalAmount * 0.05m;

                // Update reservation details
                reservation.TotalAmount = reservation.TotalAmount + surcharge;

                _dbContext.Reservations.Update(reservation);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                // Handle case where modification is not allowed
                return false;
            }
        }

        public async Task<List<Reservation>> GetReservationsByCustomerId(string customerId)
        {
            var reservations = await _dbContext.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Tour)
                .AsNoTracking()
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();

            return reservations;
        }


        public static decimal GetDiscountRate(RoomType roomType)
        {
            return roomType switch
            {
                RoomType.Single => 10,
                RoomType.Double => 20,
                RoomType.FamilySuite => 40
            };
        }

        public async Task<Reservation> GetReservationById(int reservationId)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Tour)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            return reservation;
        }
    }
}
