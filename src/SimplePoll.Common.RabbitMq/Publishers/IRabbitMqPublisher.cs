using System;

namespace SimplePoll.Common.RabbitMq.Publishers
{
    public interface IRabbitMqPublisher : IDisposable
    {
        void Publish<T>(T message, string routingKey = "");
    }
}