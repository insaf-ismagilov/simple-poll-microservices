using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using SimplePoll.Common.RabbitMq.Publishers;
using SimplePoll.Common.RabbitMq.Subscribers;

namespace SimplePoll.Common.RabbitMq.Rpc
{
    public class RpcClient : IRpcClient
    {
        private readonly ILogger<RpcClient> _logger;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private readonly IRabbitMqSubscriber _rabbitMqSubscriber;

        private const int TIMEOUT_MS = 10000;

        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingMessages;

        public RpcClient(
            ILogger<RpcClient> logger,
            IRabbitMqPublisher rabbitMqPublisher,
            IRabbitMqSubscriber rabbitMqSubscriber)
        {
            _logger = logger;
            _rabbitMqPublisher = rabbitMqPublisher;
            _rabbitMqSubscriber = rabbitMqSubscriber;

            _pendingMessages = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
        }

        public async Task<TResponse> CallAsync<TRequest, TResponse>(TRequest request, string exchangeName, string subscriberQueueName, string routingKey = "")
        {
            _rabbitMqSubscriber.Subscribe(subscriberQueueName, OnMessageReceived);

            var tcs = new TaskCompletionSource<string>();

            var ct = new CancellationTokenSource(TimeSpan.FromMilliseconds(TIMEOUT_MS));
            ct.Token.Register(() => tcs.TrySetCanceled(), false);

            var correlationId = Guid.NewGuid().ToString();
            _pendingMessages[correlationId] = tcs;

            _logger.LogInformation("RPC request {@Data}", new
            {
                CorrelationId = correlationId
            });

            _rabbitMqPublisher.Publish(request, exchangeName, routingKey, subscriberQueueName, correlationId);

            var response = await tcs.Task;

            _logger.LogInformation("Received RPC response {@Data}", new
            {
                CorrelationId = correlationId
            });

            return JsonConvert.DeserializeObject<TResponse>(response);
        }

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