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
    public class PaymentRejectedEventConsumer : IConsumeAsync<PaymentRejectedEvent>
    {
        private readonly IStockService _stockService;
        private readonly IBus _bus;
        private readonly ILogger<PaymentRejectedEventConsumer> _logger;

        public PaymentRejectedEventConsumer(IStockService stockService, IBus bus, ILogger<PaymentRejectedEventConsumer> logger)
        {
            _stockService = stockService;
            _bus = bus;
            _logger = logger;
        }

        public async Task ConsumeAsync(PaymentRejectedEvent message, CancellationToken cancellationToken = default)
        {
            await _stockService.ReleaseStocks(message.OrderId);

            await _bus.PubSub.PublishAsync(new StocksReleasedEvent
            {
                OrderId = message.OrderId,
                Reason = message.Reason
            });

            _logger.LogWarning("Stock was released at: {date}", DateTimeOffset.Now);
        }
    }
}