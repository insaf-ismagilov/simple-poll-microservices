using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Common.RabbitMq.Endpoints;
using SimplePoll.Common.RabbitMq.Rpc;
using SimplePoll.Editor.Application.Queries;

namespace SimplePoll.Editor.Rpc
{
    public class RpcServerHostedService : IHostedService
    {
        private readonly IRpcConsumer _rpcConsumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RpcServerHostedService(
            IRpcConsumer rpcConsumer,
            IServiceScopeFactory serviceScopeFactory)
        {
            _rpcConsumer = rpcConsumer;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _rpcConsumer.Subscribe(GetScopedAction<GetPollByIdQuery, ServiceResponse<PollDto>>(), RpcEndpoints.PollGetById.RequestQueue);
            _rpcConsumer.Subscribe(GetScopedAction<GetAllPollsQuery, ServiceResponse<ICollection<PollDto>>>(), RpcEndpoints.PollGetAll.RequestQueue);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Func<TRequest, Task<TResponse>> GetScopedAction<TRequest, TResponse>() where TRequest : IRequest<TResponse>
        {
            Func<TRequest, Task<TResponse>> action = query =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                return mediator.Send(query);
            };

            return action;
        }
    }
}