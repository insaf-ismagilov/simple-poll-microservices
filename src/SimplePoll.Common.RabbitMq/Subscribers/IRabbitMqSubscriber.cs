using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace SimplePoll.Common.RabbitMq.Subscribers
{
    public interface IRabbitMqSubscriber
    {
        void Subscribe(string queueName, Func<BasicDeliverEventArgs, Task<bool>> func);
    }
}