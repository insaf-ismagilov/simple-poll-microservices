using RabbitMQ.Client;

namespace SimplePoll.Common.RabbitMq.Providers
{
    public interface IRabbitMqConnectionProvider
    {
        IModel CreateConnection();
    }
}