using Microsoft.AspNetCore.Mvc;
using SimplePoll.Common.Models;

namespace SimplePoll.Common.Api
{
	public abstract class ApiControllerBase : ControllerBase
	{
		protected IActionResult CreateResponse<T>(ServiceResponse<T> serviceResponse)
		{
			if (!serviceResponse.Successful)
				return BadRequest(serviceResponse.ErrorMessage);

			if (serviceResponse.Data == null)
				return NotFound();

			return Ok(serviceResponse.Data);
		}

		protected IActionResult CreateResponse(ServiceResponse serviceResponse)
		{
			if (!serviceResponse.Successful)
				return BadRequest(serviceResponse.ErrorMessage);

			return Ok();
		}
	}
}