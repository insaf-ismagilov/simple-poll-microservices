using SimplePoll.Common.Models;
using SimplePoll.Editor.Domain.Models;

namespace SimplePoll.Editor.Application.Commands
{
	public class CreatePollCommand : SavePollCommand<ServiceResponse<PollDto>>
	{
	}
}