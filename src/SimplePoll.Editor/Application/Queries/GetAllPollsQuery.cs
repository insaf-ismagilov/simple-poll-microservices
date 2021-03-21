using System.Collections.Generic;
using MediatR;
using SimplePoll.Editor.Domain.Entities;

namespace SimplePoll.Editor.Application.Queries
{
	public class GetAllPollsQuery : IRequest<ICollection<Poll>>
	{
		
	}
}