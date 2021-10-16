using SimplePoll.Common.RabbitMq.Rpc;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public class RabbitMqRpcConsumerFactory : IRabbitMqRpcConsumerFactory
    {
        private readonly IRabbitMqPublisherFactory _publisherFactory;
        private readonly IRabbitMqSubscriberFactory _rabbitMqSubscriberFactory;

        public RabbitMqRpcConsumerFactory(
            IRabbitMqPublisherFactory publisherFactory,
            IRabbitMqSubscriberFactory subscriberFactory)
        {
            _publisherFactory = publisherFactory;
            _rabbitMqSubscriberFactory = subscriberFactory;
        }

        public IRpcConsumer Create(string publisherExchangeName, string subscriberQueueName)
        {
            var publisher = _publisherFactory.CreatePublisher(publisherExchangeName);
            var subscriber = _rabbitMqSubscriberFactory.CreateSubscriber(subscriberQueueName);

            return new RpcConsumer(publisher, subscriber);
        }
    }
}