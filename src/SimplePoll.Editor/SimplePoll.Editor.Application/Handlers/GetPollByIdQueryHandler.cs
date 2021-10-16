using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Common.Models;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Editor.Application.Queries;
using SimplePoll.Editor.Domain.Repositories;

namespace SimplePoll.Editor.Application.Handlers
{
    public class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, ServiceResponse<PollDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPollRepository _pollRepository;

        public GetPollByIdQueryHandler(
            IMapper mapper,
            IPollRepository pollRepository)
        {
            _mapper = mapper;
            _pollRepository = pollRepository;
        }

        public async Task<ServiceResponse<PollDto>> Handle(GetPollByIdQuery request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.Id);

            return ServiceResponse<PollDto>.Success(_mapper.Map<PollDto>(poll));
        }
    }
}