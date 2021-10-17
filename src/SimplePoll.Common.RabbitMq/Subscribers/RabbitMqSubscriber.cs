using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SimplePoll.Common.RabbitMq.Providers;

namespace SimplePoll.Common.RabbitMq.Subscribers
{
    public class RabbitMqSubscriber : IRabbitMqSubscriber
    {
        private readonly ILogger<RabbitMqSubscriber> _logger;
        private readonly IModel _channel;

        public RabbitMqSubscriber(
            ILogger<RabbitMqSubscriber> logger,
            IRabbitMqConnectionProvider connectionProvider)
        {
            _logger = logger;
            _channel = connectionProvider.CreateConnection();
        }

        public void Subscribe(string queueName, Func<BasicDeliverEventArgs, Task<bool>> func)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += OnReceived(queueName, func);

            _logger.LogInformation("Subscribe {@Queue}", queueName);

            _channel.BasicConsume(queueName, false, consumer);
        }

        private AsyncEventHandler<BasicDeliverEventArgs> OnReceived(string queueName, Func<BasicDeliverEventArgs, Task<bool>> func)
        {
            return async (_, ea) =>
            {
                try
                {
                    _logger.LogInformation("Received message from {@Queue}", queueName);

                    if (await func(ea))
                    {
                        _channel.BasicAck(ea.DeliveryTag, false);
                        
                        _logger.LogInformation("Acknowledged message from {@Queue}. DeliveryTag: {@DeliveryTag}", queueName, ea.DeliveryTag);
                    }
                    else
                    {
                        _channel.BasicNack(ea.DeliveryTag, false, false);
                        _logger.LogInformation("Negative acknowledged message from {@Queue}. DeliveryTag: {@DeliveryTag}", queueName, ea.DeliveryTag);
                    }
                }
                catch (Exception)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                    _logger.LogInformation("Negative acknowledged message from {@Queue}. DeliveryTag: {@DeliveryTag}", queueName, ea.DeliveryTag);
                }
            };
        }
    }
}