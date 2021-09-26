using MediatR;
using SimplePoll.Answers.Application.Models.Dto;
using SimplePoll.Answers.Domain.Entities;
using SimplePoll.Common.Models;

namespace SimplePoll.Answers.Application.Queries
{
    public class GetPollAnswerByIdQuery: IRequest<ServiceResponse<PollAnswerDto>>
    {
        public int Id { get; set; }
    }
}