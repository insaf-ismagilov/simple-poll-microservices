using System;
using System.Threading.Tasks;

namespace SimplePoll.Common.RabbitMq.Rpc
{
    public interface IRpcServer : IDisposable
    {
        void AddConsumerAction<TRequest, TResponse>(string publisherExchangeName, string subscriberQueueName, string routingKey,
            Func<TRequest, Task<TResponse>> action);
    }
}