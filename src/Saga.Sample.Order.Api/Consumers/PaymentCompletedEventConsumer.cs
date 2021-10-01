using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Saga.Sample.Services;
using Saga.Sample.Shared.Contracts;

namespace Saga.Sample.Order.Api.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumeAsync<PaymentCompletedEvent>
    {
        private readonly IOrderService _orderService;

        public PaymentCompletedEventConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task ConsumeAsync(PaymentCompletedEvent message, CancellationToken cancellationToken = default)
        {
            await _orderService.CompleteOrder(message.OrderId);
        }
    }
}