using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SimplePoll.Common.RabbitMq.Subscribers
{
    public class RabbitMqSubscriber : IRabbitMqSubscriber
    {
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMqSubscriber(IModel channel, string queueName)
        {
            _channel = channel;
            _queueName = queueName;
        }

        public void Subscribe(Func<BasicDeliverEventArgs, Task<bool>> func)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += OnReceived(func);

            _channel.BasicConsume(_queueName, false, consumer);
        }

        private AsyncEventHandler<BasicDeliverEventArgs> OnReceived(Func<BasicDeliverEventArgs, Task<bool>> func)
        {
            return async (model, ea) =>
            {
                try
                {
                    if (await func(ea))
                        _channel.BasicAck(ea.DeliveryTag, false);
                    else
                        _channel.BasicNack(ea.DeliveryTag, false, false);
                }
                catch (Exception)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                }
            };
        }

        public void Dispose()
        {
            _channel?.WaitForConfirms();
            _channel?.Close();
        }
    }
}