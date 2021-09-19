using System;
using SimplePoll.Common.Models;

namespace SimplePoll.Answers.Domain.Entities
{
    public class PollAnswer : BaseEntity<int>
    {
        public int PollId { get; set; }
        public int? PollOptionId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}