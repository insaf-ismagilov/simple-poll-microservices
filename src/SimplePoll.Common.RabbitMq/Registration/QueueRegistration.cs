using System.Collections.Generic;
using RabbitMQ.Client;

namespace SimplePoll.Common.RabbitMq.Registration
{
    public static class QueueRegistration
    {
        public static void RegisterQueue(this IModel channel, string exchangeName, string queueName, string routingKey)
        {
            var deadLetter = $"{queueName}.deadletter";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);

            channel.QueueDeclare(queueName, true, false, false, new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", exchangeName },
                { "x-dead-letter-routing-key", routingKey + RoutingKeys.Deadletter }
            });
            channel.QueueBind(queueName, exchangeName, routingKey);

            channel.QueueDeclare(deadLetter, true, false, false);
            channel.QueueBind(deadLetter, exchangeName, routingKey + RoutingKeys.Deadletter);
        }
    }
}