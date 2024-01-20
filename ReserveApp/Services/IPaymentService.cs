using ReserveApp.Data;

namespace ReserveApp.Services
{
    public interface IPaymentService
    {
        Task<Payment?> CreatePayment(Payment payment);
    }
}
