using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePoll.ApiGateway.Application.Queries;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Common.RabbitMq;
using SimplePoll.Common.RabbitMq.Endpoints;
using SimplePoll.Common.RabbitMq.Rpc;

namespace SimplePoll.ApiGateway.Application.Handlers
{
    public class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, ServiceResponse<PollDto>>
    {
        private readonly IRpcClient _rpcClient;

        public GetPollByIdQueryHandler(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public async Task<ServiceResponse<PollDto>> Handle(GetPollByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _rpcClient.CallAsync<GetPollByIdQuery, ServiceResponse<PollDto>>(request, RpcEndpoints.PollGetById.Exchange,
                RpcEndpoints.PollGetById.ResponseQueue, RoutingKeys.Request);

            return result;
        }
    }
}