using System.Collections.Generic;
using SimplePoll.Common.Enums;

namespace SimplePoll.Common.Models.Poll
{
	public class PollDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public PollStatus Status { get; set; }
		public PollType Type { get; set; }
		public ICollection<PollOptionDto> Options { get; set; }
	}
}