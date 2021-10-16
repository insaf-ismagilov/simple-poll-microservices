using System.Collections.Generic;
using MediatR;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;

namespace SimplePoll.Editor.Application.Queries
{
	public class GetAllPollsQuery : IRequest<ServiceResponse<ICollection<PollDto>>>
	{
		
	}
}