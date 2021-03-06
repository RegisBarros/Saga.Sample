using EasyNetQ;

namespace Saga.Sample.Shared.Contracts
{
    [Queue("PaymentRejectedEvent", ExchangeName = "PaymentRejected")]
    public class PaymentRejectedEvent
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
}