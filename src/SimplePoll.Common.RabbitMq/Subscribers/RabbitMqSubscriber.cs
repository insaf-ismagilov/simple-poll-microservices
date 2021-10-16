using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SimplePoll.Common.RabbitMq.Subscribers
{
    public class RabbitMqSubscriber : IRabbitMqSubscriber
    {
        private readonly ILogger<RabbitMqSubscriber> _logger;
        private readonly IModel _channel;

        public string QueueName { get; }

        public RabbitMqSubscriber(
            ILogger<RabbitMqSubscriber> logger,
            IModel channel,
            string queueName)
        {
            _logger = logger;
            _channel = channel;
            QueueName = queueName;
        }

        public void Subscribe(Func<BasicDeliverEventArgs, Task<bool>> func)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += OnReceived(func);

            _logger.LogInformation("Subscribe {@Queue}", QueueName);

            _channel.BasicConsume(QueueName, false, consumer);
        }

        private AsyncEventHandler<BasicDeliverEventArgs> OnReceived(Func<BasicDeliverEventArgs, Task<bool>> func)
        {
            return async (_, ea) =>
            {
                try
                {
                    _logger.LogInformation("Received message from {@Queue}", QueueName);

                    if (await func(ea))
                    {
                        _channel.BasicAck(ea.DeliveryTag, false);
                        
                        _logger.LogInformation("Acknowledged message from {@Queue}. DeliveryTag: {@DeliveryTag}", QueueName, ea.DeliveryTag);
                    }
                    else
                    {
                        _channel.BasicNack(ea.DeliveryTag, false, false);
                        _logger.LogInformation("Negative acknowledged message from {@Queue}. DeliveryTag: {@DeliveryTag}", QueueName, ea.DeliveryTag);
                    }
                }
                catch (Exception)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                    _logger.LogInformation("Negative acknowledged message from {@Queue}. DeliveryTag: {@DeliveryTag}", QueueName, ea.DeliveryTag);
                }
            };
        }

        public void Dispose() { }
    }
}