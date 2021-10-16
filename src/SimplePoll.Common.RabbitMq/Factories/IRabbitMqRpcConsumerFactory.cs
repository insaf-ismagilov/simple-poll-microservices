using SimplePoll.Common.RabbitMq.Rpc;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public interface IRabbitMqRpcConsumerFactory
    {
        IRpcConsumer Create(string publisherExchangeName, string subscriberQueueName);
    }
}