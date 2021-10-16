using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimplePoll.Common.RabbitMq.Factories;

namespace SimplePoll.Common.RabbitMq.Rpc
{
    public class RpcServer : IRpcServer
    {
        private readonly IRabbitMqRpcConsumerFactory _rpcConsumerFactory;

        private readonly List<IRpcConsumer> _rpcConsumers = new();

        public RpcServer(
            IRabbitMqRpcConsumerFactory rpcConsumerFactory)
        {
            _rpcConsumerFactory = rpcConsumerFactory;
        }

        public void AddConsumerAction<TRequest, TResponse>(string publisherExchangeName, string subscriberQueueName, 
            Func<TRequest, Task<TResponse>> action)
        {
            var consumer = _rpcConsumerFactory.Create(publisherExchangeName, subscriberQueueName);
            
            _rpcConsumers.Add(consumer);
            
            consumer.Subscribe(action);
        }

        public void Dispose()
        {
            _rpcConsumers.ForEach(x => x.Dispose());
        }
    }
}