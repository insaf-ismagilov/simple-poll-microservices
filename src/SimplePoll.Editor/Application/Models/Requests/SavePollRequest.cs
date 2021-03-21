using System.Collections.Generic;
using SimplePoll.Editor.Domain.Enums;

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