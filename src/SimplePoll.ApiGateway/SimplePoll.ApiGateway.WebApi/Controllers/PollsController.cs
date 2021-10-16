using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimplePoll.ApiGateway.Application.Queries;
using SimplePoll.Common.Api;

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
    }
}