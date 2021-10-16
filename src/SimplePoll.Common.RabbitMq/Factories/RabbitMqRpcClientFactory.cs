using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SimplePoll.Common.RabbitMq.Rpc;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public class RabbitMqRpcClientFactory : IRabbitMqRpcClientFactory
    {
        private readonly IRabbitMqPublisherFactory _publisherFactory;
        private readonly IRabbitMqSubscriberFactory _rabbitMqSubscriberFactory;
        private readonly ILoggerFactory _loggerFactory;

        private readonly Dictionary<(string, string), RpcClient> _rpcClients;

        public RabbitMqRpcClientFactory(
            IRabbitMqPublisherFactory publisherFactory,
            IRabbitMqSubscriberFactory subscriberFactory,
            ILoggerFactory loggerFactory)
        {
            _publisherFactory = publisherFactory;
            _rabbitMqSubscriberFactory = subscriberFactory;
            _loggerFactory = loggerFactory;

            _rpcClients = new Dictionary<(string, string), RpcClient>();
        }

        public IRpcClient Create(string publisherExchangeName, string subscriberQueueName)
        {
            if (_rpcClients.TryGetValue((publisherExchangeName, subscriberQueueName), out var rpcClient))
            {
                return rpcClient;
            }

            var publisher = _publisherFactory.CreatePublisher(publisherExchangeName);
            var subscriber = _rabbitMqSubscriberFactory.CreateSubscriber(subscriberQueueName);

            rpcClient = new RpcClient(_loggerFactory.CreateLogger<RpcClient>(), publisher, subscriber);

            _rpcClients[(publisherExchangeName, subscriberQueueName)] = rpcClient;
            
            return rpcClient;
        }
    }
}