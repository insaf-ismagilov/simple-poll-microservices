using System.Collections.Generic;
using SimplePoll.Common.Enums;
using SimplePoll.Common.Models.Poll;

namespace SimplePoll.Editor.Application.Models.Requests
{
	public abstract class SavePollRequest
	{
		public string Title { get; set; }
		public PollStatus Status { get; set; }
		public PollType Type { get; set; }
		public ICollection<PollOptionDto> Options { get; set; }
	}
}