using System.Collections.Generic;
using RabbitMQ.Client;

namespace SimplePoll.Common.RabbitMq.Registration
{
    public static class QueueRegistration
    {
        public static void RegisterQueue(this IModel channel, string exchangeName, string queueName)
        {
            var deadLetter = $"{queueName}.deadletter";
            
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
            
            channel.QueueDeclare(queueName, true, false, false, new Dictionary<string, object>
            {
                {"x-dead-letter-exchange", exchangeName},
                {"x-dead-letter-routing-key", ".deadletter"}
            });
            channel.QueueBind(queueName, exchangeName, string.Empty);

            channel.QueueDeclare(deadLetter, true, false, false);
            channel.QueueBind(deadLetter, exchangeName, ".deadletter");
        }
    }
}