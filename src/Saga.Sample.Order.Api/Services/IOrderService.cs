using System.Threading.Tasks;
using Saga.Sample.Models;

namespace Saga.Sample.Services
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderRequest request);
        Task CompleteOrder(int orderId);
        Task RejectOrderAsync(int orderId, string reason);
    }
}