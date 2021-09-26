using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Answers.Application.Models.Dto;
using SimplePoll.Answers.Application.Queries;
using SimplePoll.Answers.Domain.Repositories;
using SimplePoll.Common.Models;

namespace SimplePoll.Answers.Application.Handlers
{
    public class GetPollAnswerByIdQueryHandler : IRequestHandler<GetPollAnswerByIdQuery, ServiceResponse<PollAnswerDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPollAnswerRepository _pollAnswerRepository;

        public GetPollAnswerByIdQueryHandler(
            IMapper mapper,
            IPollAnswerRepository pollAnswerRepository)
        {
            _mapper = mapper;
            _pollAnswerRepository = pollAnswerRepository;
        }

        public async Task<ServiceResponse<PollAnswerDto>> Handle(GetPollAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            var pollAnswer = await _pollAnswerRepository.GetByIdAsync(request.Id);
            
            return ServiceResponse<PollAnswerDto>.Success(_mapper.Map<PollAnswerDto>(pollAnswer));
        }
    }
}