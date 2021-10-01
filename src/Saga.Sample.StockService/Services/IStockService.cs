using System.Threading.Tasks;

namespace Saga.Sample.StockService.Services
{
    public interface IStockService
    {
        Task ReserveStock(int orderId);
        Task<bool> ReleaseStocks(int orderId);
    }
}