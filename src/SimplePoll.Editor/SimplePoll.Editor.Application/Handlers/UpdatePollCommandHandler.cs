using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Common.Models;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Domain.Models;
using SimplePoll.Editor.Domain.Repositories;

namespace SimplePoll.Editor.Application.Handlers
{
    public class UpdatePollCommandHandler : IRequestHandler<UpdatePollCommand, ServiceResponse<PollDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPollRepository _pollRepository;

        public UpdatePollCommandHandler(
            IMapper mapper,
            IPollRepository pollRepository)
        {
            _mapper = mapper;
            _pollRepository = pollRepository;
        }

        public async Task<ServiceResponse<PollDto>> Handle(UpdatePollCommand command, CancellationToken cancellationToken)
        {
            var existingPoll = await _pollRepository.GetByIdAsync(command.Id);
            if (existingPoll is null)
                return ServiceResponse<PollDto>.Error($"Poll <{command.Id}> not found.");

            if (!existingPoll.CanBeUpdated())
                return ServiceResponse<PollDto>.Error($"Poll <{command.Id}> cannot be updated.");

            var updatedId = await _pollRepository.UpdateAsync(new PollDto
            {
                Id = command.Id,
                Title = command.Title,
                Status = command.Status,
                Type = command.Type,
                Options = command.Options
            });

            if (!updatedId.HasValue)
                return ServiceResponse<PollDto>.Error($"Poll <{command.Id}> was not updated.");

            var poll = await _pollRepository.GetByIdAsync(updatedId.Value);

            return ServiceResponse<PollDto>.Success(_mapper.Map<PollDto>(poll));
        }
    }
}