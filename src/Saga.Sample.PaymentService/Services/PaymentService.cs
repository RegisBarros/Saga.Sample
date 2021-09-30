using System;
using System.Threading.Tasks;

namespace Saga.Sample.PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<Tuple<bool, string>> DoPayment(int walletId, int userId, decimal totalAmount)
        {
            return Task.FromResult(Tuple.Create<bool, string>(true, "paid"));
        }
    }
}