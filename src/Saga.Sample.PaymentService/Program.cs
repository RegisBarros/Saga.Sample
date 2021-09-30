using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saga.Sample.PaymentService.Consumers;
using Saga.Sample.PaymentService.Services;

namespace Saga.Sample.PaymentService
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
                    services.AddScoped<IPaymentService, Services.PaymentService>();

                    var bus = RabbitHutch.CreateBus(hostContext.Configuration["RabbitMQ:ConnectionString"]);
                    
                    services.AddSingleton<IBus>(bus);
                    services.AddSingleton<MessageDispatcher>();

                    services.AddSingleton<AutoSubscriber>(_ => 
                    {
                        return new AutoSubscriber(_.GetRequiredService<IBus>(), Assembly.GetExecutingAssembly().GetName().Name) 
                        {
                            AutoSubscriberMessageDispatcher = _.GetRequiredService<MessageDispatcher>()
                        };
                    });

                    services.AddScoped<StockReservedEventConsumer>();

                    services.AddHostedService<Worker>();
                });
    }
}
