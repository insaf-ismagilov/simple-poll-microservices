using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using SimplePoll.Common.RabbitMq.Publishers;
using SimplePoll.Common.RabbitMq.Subscribers;

namespace SimplePoll.Common.RabbitMq.Clients
{
    public class RpcClient : IRpcClient
    {
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private readonly IRabbitMqSubscriber _rabbitMqSubscriber;

        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingMessages;

        public RpcClient(
            IRabbitMqPublisher rabbitMqPublisher,
            IRabbitMqSubscriber rabbitMqSubscriber)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
            _rabbitMqSubscriber = rabbitMqSubscriber;

            _pendingMessages = new ConcurrentDictionary<string, TaskCompletionSource<string>>();

            _rabbitMqSubscriber.Subscribe(OnMessageReceived);
        }

        // TODO: Add logging, timeout.
        public async Task<TResponse> CallAsync<TRequest, TResponse>(TRequest request, string routingKey = "")
        {
            var tcs = new TaskCompletionSource<string>();
            var correlationId = Guid.NewGuid().ToString();

            _pendingMessages[correlationId] = tcs;

            _rabbitMqPublisher.Publish(request, routingKey, _rabbitMqSubscriber.QueueName, correlationId);

            var response = await tcs.Task;

            return JsonConvert.DeserializeObject<TResponse>(response);
        }

        // TODO: Add logging.
        private Task<bool> OnMessageReceived(BasicDeliverEventArgs args)
        {
            var correlationId = args.BasicProperties.CorrelationId;

            var message = Encoding.UTF8.GetString(args.Body.ToArray());

            if (_pendingMessages.TryRemove(correlationId, out var tcs))
            {
                tcs.SetResult(message);
            }

            return Task.FromResult(true);
        }
    }
}