using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.Logging;
using Saga.Sample.Shared.Contracts;
using Saga.Sample.StockService.Services;

namespace Saga.Sample.StockService.Consumers
{
    public class OrderCreatedEventConsumer : IConsumeAsync<OrderCreatedEvent>
    {
        private readonly IBus _bus;
        private readonly IStockService _stockService;
        private readonly ILogger<OrderCreatedEventConsumer> _logger;

        public OrderCreatedEventConsumer(IBus bus, IStockService stockService, ILogger<OrderCreatedEventConsumer> logger)
        {
            _bus = bus;
            _stockService = stockService;
            _logger = logger;
        }

        public async Task ConsumeAsync(OrderCreatedEvent message, CancellationToken cancellationToken = default)
        {
            await _stockService.ReserveStock(message.OrderId);

            await _bus.PubSub.PublishAsync(new StockReservedEvent 
            {
                UserId = message.UserId,
                OrderId = message.OrderId,
                WalletId = message.WalletId,
                TotalAmount = message.TotalAmount
            });

            _logger.LogInformation("Stock was reserved at: {date}", DateTimeOffset.Now);
        }
    }
}