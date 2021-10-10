using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace SimplePoll.Common.RabbitMq.Publishers
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IModel _channel;
        private readonly string _exchangeName;
        
        public RabbitMqPublisher(IModel channel, string exchangeName)
        {
            _channel = channel;
            _exchangeName = exchangeName;
        }

        public void Publish<T>(T message, string routingKey = "")
        {
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            var properties = _channel.CreateBasicProperties();
            properties.DeliveryMode = 2;
            properties.ContentType = "application/json";
            
            _channel.BasicPublish(_exchangeName, routingKey, properties, bytes);
        }

        public void Dispose()
        {
            _channel?.WaitForConfirms();
            _channel?.Close();
        }
    }
}