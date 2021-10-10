using SimplePoll.Common.RabbitMq.Subscribers;

namespace SimplePoll.Common.RabbitMq.Factories
{
    public interface IRabbitMqSubscriberFactory
    {
        IRabbitMqSubscriber CreateSubscriber(string queueName);
    }
}