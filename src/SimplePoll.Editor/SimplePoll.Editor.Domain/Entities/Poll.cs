using System.Collections.Generic;
using SimplePoll.Common.Enums;
using SimplePoll.Common.Models;

namespace SimplePoll.Editor.Domain.Entities
{
	public class Poll : BaseEntity<int>
	{
		public string Title { get; set; }
		public PollStatus Status { get; set; }
		public PollType Type { get; set; }
		public ICollection<PollOption> Options { get; set; }

		public bool CanBeUpdated()
		{
			return Status == PollStatus.Draft;
		}
	}
}