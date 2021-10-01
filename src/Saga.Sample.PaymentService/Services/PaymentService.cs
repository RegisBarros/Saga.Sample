using System;
using System.Threading.Tasks;

namespace Saga.Sample.PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<Tuple<bool, string>> DoPayment(int walletId, int userId, decimal totalAmount)
        {
            var random = new Random().Next(10);

            if(random % 2 == 0)
                return Task.FromResult(Tuple.Create<bool, string>(true, "paid"));

            return Task.FromResult(Tuple.Create<bool, string>(false, "not authorize"));
        }
    }
}