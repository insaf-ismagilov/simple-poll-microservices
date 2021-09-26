using MediatR;
using SimplePoll.Common.Models;
using SimplePoll.Editor.Domain.Entities;
using SimplePoll.Editor.Domain.Models;

namespace SimplePoll.Editor.Application.Queries
{
	public class GetPollByIdQuery : IRequest<ServiceResponse<PollDto>>
	{
		public int Id { get; set; }
	}
}