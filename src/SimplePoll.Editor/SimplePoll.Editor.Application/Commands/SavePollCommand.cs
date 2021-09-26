using System.Collections.Generic;
using MediatR;
using SimplePoll.Editor.Application.Models;
using SimplePoll.Editor.Domain.Enums;
using SimplePoll.Editor.Domain.Models;

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