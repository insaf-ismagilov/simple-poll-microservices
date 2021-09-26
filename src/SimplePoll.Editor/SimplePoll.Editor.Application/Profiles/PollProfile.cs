using AutoMapper;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Application.Models;
using SimplePoll.Editor.Application.Models.Requests;
using SimplePoll.Editor.Domain.Entities;
using SimplePoll.Editor.Domain.Models;

namespace SimplePoll.Editor.Application.Profiles
{
	public class PollProfile : Profile
	{
		public PollProfile()
		{
			CreateMap<CreatePollRequest, CreatePollCommand>();
			CreateMap<UpdatePollRequest, UpdatePollCommand>();

			CreateMap<Poll, PollDto>();
			CreateMap<PollOption, PollOptionDto>();
		}
	}
}