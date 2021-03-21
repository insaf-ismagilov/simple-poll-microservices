using MediatR;
using SimplePoll.Editor.Domain.Entities;

namespace SimplePoll.Editor.Application.Queries
{
	public class GetPollByIdQuery : IRequest<Poll>
	{
		public int Id { get; set; }
	}
}