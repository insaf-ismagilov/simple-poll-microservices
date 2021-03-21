using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplePoll.Common.Api;
using SimplePoll.Editor.Application.Models;
using SimplePoll.Editor.Application.Models.Requests;
using SimplePoll.Editor.Application.Services;

namespace SimplePoll.Editor.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/editor/polls")]
	public class PollsController : ApiControllerBase
	{
		private readonly IPollService _pollService;
		
		public PollsController(IPollService pollService)
		{
			_pollService = pollService;
		}
		
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(PollDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _pollService.GetByIdAsync(id);
			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		[ProducesResponseType(typeof(ICollection<PollDto>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll()
		{
			var result = await _pollService.GetAllAsync();

			return Ok(result);
		}
		
		[HttpPost]
		[ProducesResponseType(typeof(PollDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create(CreatePollRequest request)
		{
			var result = await _pollService.CreateAsync(request);

			return CreateResponse(result);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(PollDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Update(int id, UpdatePollRequest request)
		{
			if (id != request.Id)
				return BadRequest();

			var result = await _pollService.UpdateAsync(request);

			return CreateResponse(result);
		}
	}
}