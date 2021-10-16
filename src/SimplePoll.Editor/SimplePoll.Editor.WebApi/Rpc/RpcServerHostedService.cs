using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Common.RabbitMq;
using SimplePoll.Common.RabbitMq.Endpoints;
using SimplePoll.Common.RabbitMq.Rpc;
using SimplePoll.Editor.Application.Queries;

namespace SimplePoll.Editor.Rpc
{
    public class RpcServerHostedService : IHostedService
    {
        private readonly IRpcServer _rpcServer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RpcServerHostedService(
            IRpcServer rpcServer,
            IMediator mediator,
            IServiceScopeFactory serviceScopeFactory)
        {
            _rpcServer = rpcServer;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Func<GetPollByIdQuery, Task<ServiceResponse<PollDto>>> pollGetByIdAction = query =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                return mediator.Send(query, cancellationToken);
            };

            _rpcServer.AddConsumerAction(RpcEndpoints.PollGetById.Exchange, RpcEndpoints.PollGetById.RequestQueue, RoutingKeys.Response, pollGetByIdAction);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _rpcServer.Dispose();

            return Task.CompletedTask;
        }
    }
}