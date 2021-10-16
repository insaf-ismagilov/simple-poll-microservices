﻿using System.Collections.Generic;
using SimplePoll.Editor.Domain.Enums;

namespace SimplePoll.Editor.Domain.Models
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