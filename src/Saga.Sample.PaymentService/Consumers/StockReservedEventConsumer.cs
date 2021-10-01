using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.Logging;
using Saga.Sample.PaymentService.Services;
using Saga.Sample.Shared.Contracts;

namespace Saga.Sample.PaymentService.Consumers
{
    public class StockReservedEventConsumer : IConsumeAsync<StockReservedEvent>
    {
        private readonly IPaymentService _paymentService;
        private readonly IBus _bus;
        private readonly ILogger<StockReservedEventConsumer> _logger;

        public StockReservedEventConsumer(IPaymentService paymentService, IBus bus, ILogger<StockReservedEventConsumer> logger)
        {
            _paymentService = paymentService;
            _bus = bus;
            _logger = logger;
        }

        public async Task ConsumeAsync(StockReservedEvent message, CancellationToken cancellationToken = default)
        {
            Tuple<bool, string> isPaymentCompleted = await _paymentService.DoPayment(message.WalletId, message.UserId, message.TotalAmount);

            if(isPaymentCompleted.Item1) 
            {
                await _bus.PubSub.PublishAsync(new PaymentCompletedEvent 
                {
                    OrderId = message.OrderId
                });

                _logger.LogInformation("Payment Completed at: {time}", DateTimeOffset.Now);

            } else 
            {
                await _bus.PubSub.PublishAsync(new PaymentRejectedEvent 
                { 
                    OrderId = message.OrderId,
                    Reason = isPaymentCompleted.Item2
                });

                _logger.LogCritical("Payment Rejected at: {time}", DateTimeOffset.Now);
            }
        }
    }
}