using System.Threading.Tasks;

namespace SimplePoll.Common.RabbitMq.Clients
{
    public interface IRpcClient
    {
        Task<TResponse> CallAsync<TRequest, TResponse>(TRequest request, string routingKey = "");
    }
}