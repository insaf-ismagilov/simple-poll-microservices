using MediatR;
using SimplePoll.Answers.Domain.Entities;

namespace SimplePoll.Answers.Application.Queries
{
    public class GetPollAnswerByIdQuery: IRequest<PollAnswer>
    {
        public int Id { get; set; }
    }
}