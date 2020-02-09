using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StepShop.CartApi.Notifications.ProductPriceChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StepShop.CartApi.BackgroundServices
{
    public class CartBackgroundService : BackgroundService
    {
        private readonly ILogger<CartBackgroundService> logger;
        private readonly IMediator mediator;
        private IConnection connection;
        private IModel channel;

        public CartBackgroundService(
            ILogger<CartBackgroundService> logger,
            IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += MessageRecieved;
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        private void MessageRecieved(object sender, BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            logger.LogInformation($" [x] Received {message}");
            mediator.Publish(new ProductPriceChangedNotification());
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            connection.Dispose();
            channel.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
