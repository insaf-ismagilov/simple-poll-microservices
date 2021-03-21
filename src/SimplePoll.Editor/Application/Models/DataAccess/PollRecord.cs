using SimplePoll.Editor.Domain.Enums;

namespace SimplePoll.Editor.Application.Models.DataAccess
{
	public class PollRecord
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public PollStatus Status { get; set; }
		public PollType Type { get; set; }

		public int? PollOptionId { get; set; }
		public string PollOptionText { get; set; }
		public string PollOptionValue { get; set; }
		public int? PollOptionPollId { get; set; }
	}
}