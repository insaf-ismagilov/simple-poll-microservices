using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;

namespace SimplePoll.Editor.Application.Commands
{
	public class CreatePollCommand : SavePollCommand<ServiceResponse<PollDto>>
	{
	}
}