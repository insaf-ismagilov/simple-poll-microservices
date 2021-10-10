using RabbitMQ.Client;

namespace SimplePoll.Common.RabbitMq.Registration
{
    public static class ExchangeRegistration
    {
        public static void RegisterExchange(this IModel channel, string exchangeName)
        {
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
        }
    }
}