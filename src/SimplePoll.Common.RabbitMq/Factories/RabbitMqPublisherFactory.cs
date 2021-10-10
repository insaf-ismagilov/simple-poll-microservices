using SimplePoll.Common.RabbitMq.Providers;
using SimplePoll.Common.RabbitMq.Publishers;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public class RabbitMqPublisherFactory : IRabbitMqPublisherFactory
    {
        private readonly IRabbitMqConnectionProvider _connectionProvider;
        
        public RabbitMqPublisherFactory(IRabbitMqConnectionProvider rabbitMqConnectionProvider)
        {
            _connectionProvider = rabbitMqConnectionProvider;
        }

        public IRabbitMqPublisher CreatePublisher(string exchangeName)
        {
            var channel = _connectionProvider.CreateConnection();

            return new RabbitMqPublisher(channel, exchangeName);
        }
    }
}