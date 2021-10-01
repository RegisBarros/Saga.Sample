using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using Saga.Sample.Models;
using Saga.Sample.Shared.Contracts;

namespace Saga.Sample.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBus _bus;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IBus bus, ILogger<OrderService> logger)
        {
            _bus = bus;
            _logger = logger;
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

            _logger.LogInformation("Order Created at: {time}", DateTimeOffset.Now);
        }

        public Task CompleteOrder(int orderId)
        {
            _logger.LogInformation("Order Completed at: {time}", DateTimeOffset.Now);
            
            return Task.CompletedTask;
        }

        public Task RejectOrderAsync(int orderId, string reason)
        {
            _logger.LogWarning("Order Rejected at: {time}", DateTimeOffset.Now);

            return Task.CompletedTask;
        }
    }
}