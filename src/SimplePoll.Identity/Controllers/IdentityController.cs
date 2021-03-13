using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplePoll.Identity.Application.Models;
using SimplePoll.Identity.Application.Models.Requests;
using SimplePoll.Identity.Application.Services;

namespace SimplePoll.Identity.Controllers
{
	[ApiController]
	[Route("api/identity")]
	public class IdentityController : ControllerBase
	{
		private readonly IIdentityService _identityService;

		public IdentityController(IIdentityService identityService)
		{
			_identityService = identityService;
		}
		
		[AllowAnonymous]
		[HttpPost("signin")]
		[ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> SignInAsync(SignInRequest request)
		{
			var result = await _identityService.SignInAsync(request);

			if (!result.Successful)
				return Unauthorized();

			return Ok(result.Data);
		}
		
		[AllowAnonymous]
		[HttpPost("signup")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> SignUpAsync(SignUpRequest request)
		{
			var result = await _identityService.SignUpAsync(request);

			if (!result.Successful)
				return BadRequest(result.ErrorMessage);

			return Ok();
		}
	}
}