using MediatR;
using SimplePoll.Answers.Application.Models.Dto;
using SimplePoll.Common.Models;

namespace SimplePoll.Answers.Application.Commands
{
    public class CreatePollAnswerCommand : IRequest<ServiceResponse<PollAnswerDto>>
    {
        public int PollId { get; set; }
        public int UserId { get; set; }
        public int PollOptionId { get; set; }
    }
}