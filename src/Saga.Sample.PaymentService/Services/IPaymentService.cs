using System;
using System.Threading.Tasks;

namespace Saga.Sample.PaymentService.Services
{
    public interface IPaymentService
    {
        Task<Tuple<bool, string>> DoPayment(int walletId, int userId, decimal totalAmount);
    }
}