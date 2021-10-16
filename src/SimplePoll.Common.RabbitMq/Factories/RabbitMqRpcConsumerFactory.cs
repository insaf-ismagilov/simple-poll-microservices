using Microsoft.Extensions.Logging;
using SimplePoll.Common.RabbitMq.Rpc;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public class RabbitMqRpcConsumerFactory : IRabbitMqRpcConsumerFactory
    {
        private readonly IRabbitMqPublisherFactory _publisherFactory;
        private readonly IRabbitMqSubscriberFactory _rabbitMqSubscriberFactory;
        private readonly ILoggerFactory _loggerFactory;

        public RabbitMqRpcConsumerFactory(
            IRabbitMqPublisherFactory publisherFactory,
            IRabbitMqSubscriberFactory subscriberFactory,
            ILoggerFactory loggerFactory)
        {
            _publisherFactory = publisherFactory;
            _rabbitMqSubscriberFactory = subscriberFactory;
            _loggerFactory = loggerFactory;
        }

        public IRpcConsumer Create(string publisherExchangeName, string subscriberQueueName)
        {
            var publisher = _publisherFactory.CreatePublisher(publisherExchangeName);
            var subscriber = _rabbitMqSubscriberFactory.CreateSubscriber(subscriberQueueName);

            return new RpcConsumer(_loggerFactory.CreateLogger<RpcConsumer>(), publisher, subscriber);
        }
    }
}