using System;
using System.Threading.Tasks;

namespace SimplePoll.Common.RabbitMq.Rpc
{
    public interface IRpcConsumer
    {
        void Subscribe<TRequest, TResponse>(Func<TRequest, Task<TResponse>> action);
    }
}