using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplePoll.ApiGateway.Application.Queries;
using SimplePoll.Common.Api;
using SimplePoll.Common.Models.Poll;

namespace SimplePoll.ApiGateway.WebApi.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/polls")]
    public class PollsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        
        public PollsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPollByIdQuery
            {
                Id = id
            });
            
            return CreateResponse(result);
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PollDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPollsQuery());

            return Ok(result);
        }
    }
}