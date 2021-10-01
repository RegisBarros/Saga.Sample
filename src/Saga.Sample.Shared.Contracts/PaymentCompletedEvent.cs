using EasyNetQ;

namespace Saga.Sample.Shared.Contracts
{
    [Queue("PaymentCompletedEvent", ExchangeName = "PaymentCompleted")]
    public class PaymentCompletedEvent
    {
        public int OrderId { get; set; }
    }
}