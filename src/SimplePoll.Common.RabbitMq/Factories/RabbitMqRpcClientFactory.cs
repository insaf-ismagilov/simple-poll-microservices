using SimplePoll.Common.RabbitMq.Rpc;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public class RabbitMqRpcClientFactory : IRabbitMqRpcClientFactory
    {
        private readonly IRabbitMqPublisherFactory _publisherFactory;
        private readonly IRabbitMqSubscriberFactory _rabbitMqSubscriberFactory;
        
        public RabbitMqRpcClientFactory(
            IRabbitMqPublisherFactory publisherFactory,
            IRabbitMqSubscriberFactory subscriberFactory)
        {
            _publisherFactory = publisherFactory;
            _rabbitMqSubscriberFactory = subscriberFactory;
        }

        public IRpcClient Create(string publisherExchangeName, string subscriberQueueName)
        {
            var publisher = _publisherFactory.CreatePublisher(publisherExchangeName);
            var subscriber = _rabbitMqSubscriberFactory.CreateSubscriber(subscriberQueueName);
            
            return new RpcClient(publisher, subscriber);
        }
    }
}