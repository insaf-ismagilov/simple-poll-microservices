using System;
using RabbitMQ.Client;

namespace SimplePoll.Common.RabbitMq.Publishers
{
    public interface IRabbitMqPublisher
    {
        void Publish<T>(T message, string exchangeName, string routingKey = "", string replyTo = null, string correlationId = null);
    }
}