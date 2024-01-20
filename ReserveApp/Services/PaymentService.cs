using ReserveApp.Data;

namespace ReserveApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Payment?> CreatePayment(Payment payment)
        {
            await _dbContext.Payments.AddAsync(payment);
            
            var addedRow = await _dbContext.SaveChangesAsync();
            return addedRow > 0 ? payment : null;
        }
    }
}
