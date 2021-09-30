using System.Threading.Tasks;

namespace Saga.Sample.StockService.Services
{
    public class StockService : IStockService
    {
        public Task<bool> ReleaseStock(int orderId)
        {
            return Task.FromResult(true);
        }

        public Task ReserveStock(int orderId)
        {
            return Task.CompletedTask;
        }
    }
}