using System.Collections.Generic;

namespace SimplePoll.Answers.Application.Models.Dto
{
    public class PollAnswerDto
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public int UserId { get; set; }
        public int PollOptionId { get; set; }
    }
}