using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplePoll.Common.Api;
using SimplePoll.Common.Models.Poll;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Application.Models.Requests;
using SimplePoll.Editor.Application.Queries;

namespace SimplePoll.Editor.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/editor/polls")]
    public class PollsController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PollsController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PollDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPollByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PollDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPollsQuery());

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PollDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreatePollRequest request)
        {
            var command = _mapper.Map<CreatePollCommand>(request);
            var result = await _mediator.Send(command);

            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PollDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UpdatePollRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            var command = _mapper.Map<UpdatePollCommand>(request);

            var result = await _mediator.Send(command);

            return CreateResponse(result);
        }
    }
}