using System.Collections.Generic;
using MediatR;
using SimplePoll.Common.Enums;
using SimplePoll.Common.Models.Poll;

namespace SimplePoll.Editor.Application.Commands
{
	public abstract class SavePollCommand<T> : IRequest<T>
	{
		public string Title { get; set; }
		public PollStatus Status { get; set; }
		public PollType Type { get; set; }
		public ICollection<PollOptionDto> Options { get; set; }
	}
}