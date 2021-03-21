using System.Collections.Generic;
using SimplePoll.Editor.Application.Models.DataAccess;
using SimplePoll.Editor.Domain.Entities;

namespace SimplePoll.Editor.Application.Extensions
{
	public static class PollMappingHelper
	{
		public static IEnumerable<Poll> ToPoll(this IEnumerable<PollRecord> pollRecords)
		{
			var polls = new Dictionary<int, Poll>();

			foreach (var pollRecord in pollRecords)
			{
				if (!polls.TryGetValue(pollRecord.Id, out var poll))
				{
					poll = new Poll
					{
						Id = pollRecord.Id,
						Title = pollRecord.Title,
						Status = pollRecord.Status,
						Type = pollRecord.Type,
						Options = new List<PollOption>()
					};

					polls[pollRecord.Id] = poll;
				}

				if (pollRecord.PollOptionId.HasValue)
					poll.Options.Add(new PollOption
					{
						Id = pollRecord.PollOptionId.Value,
						Text = pollRecord.PollOptionText,
						Value = pollRecord.PollOptionValue,
						PollId = pollRecord.PollOptionPollId.GetValueOrDefault()
					});
			}

			return polls.Values;
		}
	}
}