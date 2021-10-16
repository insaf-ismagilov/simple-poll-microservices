using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace SimplePoll.Common.RabbitMq.Publishers
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IModel _channel;

        public string ExchangeName { get; }

        public RabbitMqPublisher(IModel channel, string exchangeName)
        {
            _channel = channel;
            ExchangeName = exchangeName;
        }

        public void Publish<T>(T message, string routingKey = "", string replyTo = null, string correlationId = null)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            var properties = _channel.CreateBasicProperties();
            properties.DeliveryMode = 2;
            properties.ContentType = "application/json";
            properties.ReplyTo = replyTo;
            properties.CorrelationId = correlationId;

            _channel.BasicPublish(ExchangeName, routingKey, properties, bytes);
        }

        public void Dispose()
        {
        }
    }
}