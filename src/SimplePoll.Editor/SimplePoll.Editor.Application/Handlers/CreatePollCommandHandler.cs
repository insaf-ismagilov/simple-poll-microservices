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
    public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, ServiceResponse<PollDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPollRepository _pollRepository;

        public CreatePollCommandHandler(
            IMapper mapper,
            IPollRepository pollRepository)
        {
            _mapper = mapper;
            _pollRepository = pollRepository;
        }

        public async Task<ServiceResponse<PollDto>> Handle(CreatePollCommand request, CancellationToken cancellationToken)
        {
            var createdId = await _pollRepository.CreateAsync(new PollDto
            {
                Title = request.Title,
                Status = request.Status,
                Type = request.Type,
                Options = request.Options
            });

            var poll = await _pollRepository.GetByIdAsync(createdId);

            return ServiceResponse<PollDto>.Success(_mapper.Map<PollDto>(poll));
        }
    }
}