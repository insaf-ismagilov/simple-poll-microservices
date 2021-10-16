using MediatR;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;

namespace SimplePoll.ApiGateway.Application.Queries
{
	public class GetPollByIdQuery : IRequest<ServiceResponse<PollDto>>
	{
		public int Id { get; set; }
	}
}