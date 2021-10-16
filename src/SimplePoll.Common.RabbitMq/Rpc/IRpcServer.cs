using System;
using System.Threading.Tasks;

namespace SimplePoll.Common.RabbitMq.Rpc
{
    public interface IRpcServer : IDisposable
    {
        void AddConsumerAction<TRequest, TResponse>(string publisherExchangeName, string subscriberQueueName,
            Func<TRequest, Task<TResponse>> action);
    }
}