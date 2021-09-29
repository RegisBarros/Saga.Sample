using System.Threading.Tasks;
using EasyNetQ;
using Saga.Sample.Models;
using Saga.Sample.Shared.Contracts;

namespace Saga.Sample.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBus _bus;

        public OrderService(IBus bus)
        {
            _bus = bus;
        }

        public async Task CreateOrder(CreateOrderRequest request)
        {
            await _bus.PubSub.PublishAsync(new OrderCreatedEvent 
            {
                UserId = request.UserId,
                OrderId = 1,
                WalletId = request.WalletId,
                TotalAmount = request.TotalAmount
            });
        }

        public Task CompleteOrder(int orderId)
        {
            return Task.CompletedTask;
        }

        public Task RejectOrderAsync(int orderId, string reason)
        {
            return Task.CompletedTask;
        }
    }
}