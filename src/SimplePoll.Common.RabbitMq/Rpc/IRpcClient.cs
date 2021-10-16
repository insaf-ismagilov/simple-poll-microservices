using System;
using System.Threading.Tasks;

namespace SimplePoll.Common.RabbitMq.Rpc
{
    public interface IRpcClient : IDisposable
    {
        Task<TResponse> CallAsync<TRequest, TResponse>(TRequest request, string routingKey = "");
    }
}