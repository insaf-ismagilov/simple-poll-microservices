using System;
using System.Collections.Generic;
using SimplePoll.Common.Models;

namespace SimplePoll.Answers.Domain.Entities
{
    public class PollAnswer : BaseEntity<int>
    {
        public int PollId { get; set; }
        public int PollOptionId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}