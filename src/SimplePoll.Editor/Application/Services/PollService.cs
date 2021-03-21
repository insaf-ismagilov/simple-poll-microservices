using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Common.Models;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Application.Models;
using SimplePoll.Editor.Application.Models.Requests;
using SimplePoll.Editor.Application.Queries;

namespace SimplePoll.Editor.Application.Services
{
	public class PollService : IPollService
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public PollService(
			IMapper mapper,
			IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		public async Task<ServiceResponse<PollDto>> CreateAsync(CreatePollRequest request)
		{
			var command = _mapper.Map<CreatePollCommand>(request);

			var newId = await _mediator.Send(command);

			var pollDto = await GetByIdAsync(newId);
			
			return ServiceResponse<PollDto>.Success(pollDto);
		}

		public async Task<ServiceResponse<PollDto>> UpdateAsync(UpdatePollRequest request)
		{
			var command = _mapper.Map<UpdatePollCommand>(request);

			var existingPoll = await _mediator.Send(new GetPollByIdQuery {Id = command.Id});
			if(existingPoll == null)
				return ServiceResponse<PollDto>.Error($"Poll <{command.Id}> not found.");
			
			if(!existingPoll.CanBeUpdated())
				return ServiceResponse<PollDto>.Error($"Poll <{command.Id}> cannot be updated.");

			var updatedId = await _mediator.Send(command);

			var pollDto = updatedId.HasValue ? await GetByIdAsync(updatedId.Value) : null;
			
			return ServiceResponse<PollDto>.Success(pollDto);
		}

		public async Task<PollDto> GetByIdAsync(int id)
		{
			var poll = await _mediator.Send(new GetPollByIdQuery {Id = id});
			
			return _mapper.Map<PollDto>(poll);
		}

		public async Task<ICollection<PollDto>> GetAllAsync()
		{
			var polls = await _mediator.Send(new GetAllPollsQuery());
			
			return _mapper.Map<ICollection<PollDto>>(polls);
		}
	}
}