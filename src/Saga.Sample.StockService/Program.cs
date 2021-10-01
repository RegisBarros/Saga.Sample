using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saga.Sample.StockService.Consumers;
using Saga.Sample.StockService.Services;

namespace Saga.Sample.StockService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var bus = RabbitHutch.CreateBus(hostContext.Configuration["RabbitMQ:ConnectionString"]);

                    services.AddScoped<IStockService, Services.StockService>();
                    services.AddSingleton<MessageDispatcher>();
                    services.AddSingleton<IBus>(bus);

                    services.AddSingleton<AutoSubscriber>(_ => 
                    {
                        return new AutoSubscriber(_.GetRequiredService<IBus>(), Assembly.GetExecutingAssembly().GetName().Name) {
                            AutoSubscriberMessageDispatcher = _.GetRequiredService<MessageDispatcher>()
                        };
                    });
                    
                    services.AddScoped<OrderCreatedEventConsumer>();
                    services.AddScoped<PaymentRejectedEventConsumer>();
                    
                    services.AddHostedService<Worker>();
                });
    }
}
