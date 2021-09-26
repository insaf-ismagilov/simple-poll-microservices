namespace SimplePoll.Answers.Application.Models.Requests
{
    public class CreatePollAnswerRequest
    {
        public int PollId { get; set; }
        public int UserId { get; set; }
        public int PollOptionId { get; set; }
    }
}