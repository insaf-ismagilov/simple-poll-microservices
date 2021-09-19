using AutoMapper;
using SimplePoll.Answers.Application.Commands;
using SimplePoll.Answers.Application.Models.Dto;
using SimplePoll.Answers.Application.Models.Requests;
using SimplePoll.Answers.Domain.Entities;

namespace SimplePoll.Answers.Application.Profiles
{
    public class PollAnswerProfile : Profile
    {
        public PollAnswerProfile()
        {
            CreateMap<CreatePollAnswerRequest, CreatePollAnswerCommand>();
            CreateMap<CreatePollAnswerCommand, PollAnswer>()
                .ForMember(m => m.CreatedDate, o => o.Ignore())
                .ForMember(m => m.LastModifiedDate, o => o.Ignore())
                .ForMember(m => m.Id, o => o.Ignore());
            CreateMap<PollAnswer, PollAnswerDto>()
                .ReverseMap()
                .ForMember(m => m.CreatedDate, o => o.Ignore())
                .ForMember(m => m.LastModifiedDate, o => o.Ignore());
        }
    }
}