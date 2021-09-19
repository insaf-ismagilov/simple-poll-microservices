using MediatR;

namespace SimplePoll.Answers.Application.Commands
{
    public class CreatePollAnswerCommand : IRequest<int>
    {
        public int PollId { get; set; }
        public int UserId { get; set; }
        public int PollOptionId { get; set; }
    }
}