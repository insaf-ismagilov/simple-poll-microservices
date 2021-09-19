using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimplePoll.Answers.Application.Commands;
using SimplePoll.Answers.Application.Models.Dto;
using SimplePoll.Answers.Application.Models.Requests;
using SimplePoll.Answers.Application.Queries;
using SimplePoll.Common.Models;

namespace SimplePoll.Answers.Application.Services
{
    public class PollAnswerService : IPollAnswerService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PollAnswerService(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PollAnswerDto>> GetByIdAsync(int id)
        {
            var pollAnswer = await _mediator.Send(new GetPollAnswerByIdQuery { Id = id });

            return ServiceResponse<PollAnswerDto>.Success(_mapper.Map<PollAnswerDto>(pollAnswer));
        }

        public async Task<ServiceResponse<PollAnswerDto>> CreateAsync(CreatePollAnswerRequest request)
        {
            var command = _mapper.Map<CreatePollAnswerCommand>(request);

            var id = await _mediator.Send(command);

            return await GetByIdAsync(id);
        }
    }
}