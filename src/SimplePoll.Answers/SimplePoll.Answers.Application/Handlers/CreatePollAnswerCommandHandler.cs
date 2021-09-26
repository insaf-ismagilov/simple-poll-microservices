using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Answers.Application.Commands;
using SimplePoll.Answers.Application.Models.Dto;
using SimplePoll.Answers.Domain.Entities;
using SimplePoll.Answers.Domain.Repositories;
using SimplePoll.Common.Models;

namespace SimplePoll.Answers.Application.Handlers
{
    public class CreatePollAnswerCommandHandler : IRequestHandler<CreatePollAnswerCommand, ServiceResponse<PollAnswerDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPollAnswerRepository _pollAnswerRepository;

        public CreatePollAnswerCommandHandler(
            IMapper mapper,
            IPollAnswerRepository pollAnswerRepository)
        {
            _mapper = mapper;
            _pollAnswerRepository = pollAnswerRepository;
        }
        
        public async Task<ServiceResponse<PollAnswerDto>> Handle(CreatePollAnswerCommand command, CancellationToken cancellationToken)
        {
            var pollAnswerToCreate = _mapper.Map<PollAnswer>(command);

            var createdPollAnswer = await _pollAnswerRepository.CreateAsync(pollAnswerToCreate);
            
            return ServiceResponse<PollAnswerDto>.Success(_mapper.Map<PollAnswerDto>(createdPollAnswer));
        }
    }
}