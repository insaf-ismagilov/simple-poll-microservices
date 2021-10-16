using Microsoft.Extensions.Logging;
using SimplePoll.Common.RabbitMq.Providers;
using SimplePoll.Common.RabbitMq.Subscribers;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public class RabbitMqSubscriberFactory : IRabbitMqSubscriberFactory
    {
        private readonly IRabbitMqConnectionProvider _connectionProvider;
        private readonly ILoggerFactory _loggerFactory;

        public RabbitMqSubscriberFactory(
            IRabbitMqConnectionProvider connectionProvider,
            ILoggerFactory loggerFactory)
        {
            _connectionProvider = connectionProvider;
            _loggerFactory = loggerFactory;
        }

        public IRabbitMqSubscriber CreateSubscriber(string queueName)
        {
            var channel = _connectionProvider.CreateConnection();

            return new RabbitMqSubscriber(_loggerFactory.CreateLogger<RabbitMqSubscriber>(), channel, queueName);
        }
    }
}