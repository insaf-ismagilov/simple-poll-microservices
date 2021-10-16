using System.Threading.Tasks;

namespace SimplePoll.Common.RabbitMq.Rpc
{
    public interface IRpcClient
    {
        Task<TResponse> CallAsync<TRequest, TResponse>(TRequest request, string routingKey = "");
    }
}