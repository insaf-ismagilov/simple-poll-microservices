using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SimplePoll.Common.RabbitMq.Providers;

namespace SimplePoll.Common.RabbitMq.Publishers
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly ILogger<RabbitMqPublisher> _logger;
        private readonly IModel _channel;

        public RabbitMqPublisher(
            ILogger<RabbitMqPublisher> logger,
            IRabbitMqConnectionProvider connectionProvider)
        {
            _logger = logger;
            _channel = connectionProvider.CreateConnection();
        }

        public void Publish<T>(T message, string exchangeName, string routingKey = "", string replyTo = null, string correlationId = null)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            var properties = _channel.CreateBasicProperties();
            properties.DeliveryMode = 2;
            properties.ContentType = "application/json";
            properties.ReplyTo = replyTo;
            properties.CorrelationId = correlationId;

            _logger.LogInformation("Publishing message to exchange {@Data}", new
            {
                ExchangeName = exchangeName,
                RoutingKey = routingKey,
                ReplyTo = replyTo,
                CorrelationId = correlationId
            });
            
            _channel.BasicPublish(exchangeName, routingKey, properties, bytes);
        }
    }
}