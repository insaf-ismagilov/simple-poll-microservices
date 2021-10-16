using SimplePoll.Common.RabbitMq.Rpc;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public interface IRabbitMqRpcClientFactory
    {
        IRpcClient Create(string publisherExchangeName, string subscriberQueueName);
    }
}