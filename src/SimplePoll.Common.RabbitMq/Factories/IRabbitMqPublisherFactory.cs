using SimplePoll.Common.RabbitMq.Publishers;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public interface IRabbitMqPublisherFactory
    {
        IRabbitMqPublisher CreatePublisher(string exchangeName);
    }
}