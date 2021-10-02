using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplePoll.Identity.Application.Commands;
using SimplePoll.Identity.Application.Models;
using SimplePoll.Identity.Application.Models.Requests;

namespace SimplePoll.Identity.WebApi.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignInAsync(SignInRequest request)
        {
            var result = await _mediator.Send(new SignInCommand
            {
                Email = request.Email,
                Password = request.Password
            });

            if (!result.Successful)
                return Unauthorized();

            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> SignUpAsync(SignUpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}