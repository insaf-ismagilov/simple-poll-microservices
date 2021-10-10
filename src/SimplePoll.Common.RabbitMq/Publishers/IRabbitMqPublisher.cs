using System;
using RabbitMQ.Client;

namespace SimplePoll.Common.RabbitMq.Publishers
{
    public interface IRabbitMqPublisher : IDisposable
    {
        string ExchangeName { get; }
        void Publish<T>(T message, string routingKey = "", string replyTo = null, string correlationId = null);
    }
}