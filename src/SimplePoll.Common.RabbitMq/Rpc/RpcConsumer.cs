using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using SimplePoll.Common.RabbitMq.Publishers;
using SimplePoll.Common.RabbitMq.Subscribers;

namespace SimplePoll.Common.RabbitMq.Rpc
{
    public class RpcConsumer : IRpcConsumer
    {
        private readonly ILogger<RpcConsumer> _logger;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private readonly IRabbitMqSubscriber _rabbitMqSubscriber;

        public RpcConsumer(
            ILogger<RpcConsumer> logger,
            IRabbitMqPublisher rabbitMqPublisher,
            IRabbitMqSubscriber rabbitMqSubscriber)
        {
            _logger = logger;
            _rabbitMqPublisher = rabbitMqPublisher;
            _rabbitMqSubscriber = rabbitMqSubscriber;
        }

        public void Subscribe<TRequest, TResponse>(Func<TRequest, Task<TResponse>> action, string routingKey)
        {
            _rabbitMqSubscriber.Subscribe(args => OnMessageReceived(args, action, routingKey));
        }

        private async Task<bool> OnMessageReceived<TRequest, TResponse>(BasicDeliverEventArgs args, Func<TRequest, Task<TResponse>> action, string routingKey)
        {
            _logger.LogInformation("Received RPC request {@Data}", new
            {
                CorrelationId = args.BasicProperties.CorrelationId
            });

            var messageString = Encoding.UTF8.GetString(args.Body.ToArray());

            var request = JsonConvert.DeserializeObject<TRequest>(messageString);

            var response = await action(request);

            _rabbitMqPublisher.Publish(response, routingKey, correlationId: args.BasicProperties.CorrelationId);

            _logger.LogInformation("Published RPC response {@Data}", new
            {
                CorrelationId = args.BasicProperties.CorrelationId
            });

            return true;
        }

        public void Dispose()
        {
            _rabbitMqPublisher?.Dispose();
            _rabbitMqSubscriber?.Dispose();
        }
    }
}