using System.Collections.Generic;
using MediatR;
using SimplePoll.Editor.Application.Models;
using SimplePoll.Editor.Domain.Enums;

namespace SimplePoll.Editor.Application.Commands
{
	public class UpdatePollCommand : IRequest<int?>
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public PollStatus Status { get; set; }
		public PollType Type { get; set; }
		public ICollection<PollOptionDto> Options { get; set; }
	}
}