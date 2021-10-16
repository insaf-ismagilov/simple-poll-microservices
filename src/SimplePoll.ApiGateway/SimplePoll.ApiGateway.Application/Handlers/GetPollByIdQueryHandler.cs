using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePoll.ApiGateway.Application.Queries;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Common.RabbitMq;
using SimplePoll.Common.RabbitMq.Endpoints;
using SimplePoll.Common.RabbitMq.Factories;

namespace SimplePoll.ApiGateway.Application.Handlers
{
    public class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, ServiceResponse<PollDto>>
    {
        private readonly IRabbitMqRpcClientFactory _rpcClientFactory;

        public GetPollByIdQueryHandler(IRabbitMqRpcClientFactory rpcClientFactory)
        {
            _rpcClientFactory = rpcClientFactory;
        }
        
        public async Task<ServiceResponse<PollDto>> Handle(GetPollByIdQuery request, CancellationToken cancellationToken)
        {
            using var client = _rpcClientFactory.Create(RpcEndpoints.PollGetById.Exchange, RpcEndpoints.PollGetById.ResponseQueue);

            var result = await client.CallAsync<GetPollByIdQuery, ServiceResponse<PollDto>>(request, RoutingKeys.Request);
            
            return result;
        }
    }
}