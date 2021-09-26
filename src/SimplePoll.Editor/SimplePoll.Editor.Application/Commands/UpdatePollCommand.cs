using SimplePoll.Common.Models;
using SimplePoll.Editor.Domain.Models;

namespace SimplePoll.Editor.Application.Commands
{
	public class UpdatePollCommand : SavePollCommand<ServiceResponse<PollDto>>
	{
		public int Id { get; set; }
	}
}