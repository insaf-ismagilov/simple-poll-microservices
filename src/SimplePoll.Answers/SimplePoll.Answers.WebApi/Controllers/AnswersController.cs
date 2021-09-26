using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimplePoll.Answers.Application.Commands;
using SimplePoll.Answers.Application.Models.Requests;
using SimplePoll.Answers.Application.Queries;
using SimplePoll.Common.Api;

namespace SimplePoll.Answers.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class AnswersController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AnswersController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPollAnswerByIdQuery { Id = id });

            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(CreatePollAnswerRequest request)
        {
            var command = _mapper.Map<CreatePollAnswerCommand>(request);
            var result = await _mediator.Send(command);

            return CreateResponse(result);
        }
    }
}