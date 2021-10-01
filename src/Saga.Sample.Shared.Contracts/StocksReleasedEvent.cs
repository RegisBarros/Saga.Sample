using EasyNetQ;

namespace Saga.Sample.Shared.Contracts
{
    [Queue("StocksReleasedEvent", ExchangeName = "StocksReleased")]
    public class StocksReleasedEvent 
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
}