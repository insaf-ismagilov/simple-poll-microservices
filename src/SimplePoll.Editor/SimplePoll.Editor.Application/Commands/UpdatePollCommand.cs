using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;

namespace SimplePoll.Editor.Application.Commands
{
	public class UpdatePollCommand : SavePollCommand<ServiceResponse<PollDto>>
	{
		public int Id { get; set; }
	}
}