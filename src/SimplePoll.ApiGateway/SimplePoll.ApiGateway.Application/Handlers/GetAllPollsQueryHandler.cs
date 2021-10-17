using System.Collections.Generic;
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
    public class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, ServiceResponse<ICollection<PollDto>>>
    {
        private readonly IRpcClient _rpcClient;

        public GetAllPollsQueryHandler(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public async Task<ServiceResponse<ICollection<PollDto>>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
        {
            var result = await _rpcClient.CallAsync<GetAllPollsQuery, ServiceResponse<ICollection<PollDto>>>(request, RpcEndpoints.PollGetAll.Exchange,
                RpcEndpoints.PollGetById.ResponseQueue, RoutingKeys.Request);

            return result;
        }
    }
}