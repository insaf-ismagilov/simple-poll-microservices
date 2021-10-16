using Microsoft.Extensions.Logging;
using SimplePoll.Common.RabbitMq.Providers;
using SimplePoll.Common.RabbitMq.Publishers;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public class RabbitMqPublisherFactory : IRabbitMqPublisherFactory
    {
        private readonly IRabbitMqConnectionProvider _connectionProvider;
        private readonly ILoggerFactory _loggerFactory;
        
        public RabbitMqPublisherFactory(
            IRabbitMqConnectionProvider rabbitMqConnectionProvider,
            ILoggerFactory loggerFactory)
        {
            
            _connectionProvider = rabbitMqConnectionProvider;
            _loggerFactory = loggerFactory;
        }

        public IRabbitMqPublisher CreatePublisher(string exchangeName)
        {
            var channel = _connectionProvider.CreateConnection();

            return new RabbitMqPublisher(_loggerFactory.CreateLogger<RabbitMqPublisher>(), channel, exchangeName);
        }
    }
}