using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerHostedService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "hello";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken) 
        {
            var _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,  
                DispatchConsumersAsync = true
            };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclarePassive(QueueName);
            _channel.BasicQos(0, 1, false);

            _logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            
            var consumer = new AsyncEventingBasicConsumer(_channel);
            
            consumer.Received += (bc, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.LogInformation($"Processing msg: '{message}'.");
                _channel.BasicAck(ea.DeliveryTag, false);
                return Task.CompletedTask;
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }
    }
}
