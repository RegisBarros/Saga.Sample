using EasyNetQ;
namespace Saga.Sample.Shared.Contracts
{
    [Queue("OrderCreatedEvent", ExchangeName = "OrderCreated")]
    public class OrderCreatedEvent
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int WalletId { get; set; }
        public decimal TotalAmount { get; set; } 
    }
}
