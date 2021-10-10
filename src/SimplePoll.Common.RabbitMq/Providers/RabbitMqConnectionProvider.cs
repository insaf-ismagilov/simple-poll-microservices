using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SimplePoll.Common.RabbitMq.Configurations;

namespace SimplePoll.Common.RabbitMq.Providers
{
    
    public class RabbitMqConnectionProvider : IRabbitMqConnectionProvider
    {
        private readonly IOptions<RabbitMqConfiguration> _options;

        private IModel _channel;

        public RabbitMqConnectionProvider(IOptions<RabbitMqConfiguration> options)
        {
            _options = options;
        }

        public IModel CreateConnection()
        {
            EnsureChannelCreated();

            return _channel;
        }

        private void EnsureChannelCreated()
        {
            if (_channel is not null)
                return;

            var connectionFactory = new ConnectionFactory
            {
                HostName = _options.Value.HostName,
                VirtualHost = _options.Value.VHost,
                DispatchConsumersAsync = true
            };

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            _channel = channel;
        }
    }
}