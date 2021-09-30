namespace Saga.Sample.Shared.Contracts
{
    public class StockReservedEvent
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int WalletId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}