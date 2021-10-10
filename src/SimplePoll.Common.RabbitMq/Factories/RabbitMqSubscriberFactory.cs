using SimplePoll.Common.RabbitMq.Providers;
using SimplePoll.Common.RabbitMq.Subscribers;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public class RabbitMqSubscriberFactory : IRabbitMqSubscriberFactory
    {
        private readonly IRabbitMqConnectionProvider _connectionProvider;

        public RabbitMqSubscriberFactory(IRabbitMqConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public IRabbitMqSubscriber CreateSubscriber(string queueName)
        {
            var channel = _connectionProvider.CreateConnection();

            return new RabbitMqSubscriber(channel, queueName);
        }
    }
}