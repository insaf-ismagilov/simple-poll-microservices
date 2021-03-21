using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SimplePoll.Common.Middlewares
{
	public class ErrorsHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorsHandlerMiddleware> _logger;

		public ErrorsHandlerMiddleware(RequestDelegate next, ILogger<ErrorsHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				await WriteValidationExceptionAsync(context, ex);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task WriteValidationExceptionAsync(HttpContext context, ValidationException ex)
		{
			_logger.LogError(ex, "{Message}", ex.Message);

			context.Response.StatusCode = StatusCodes.Status400BadRequest;

			var errors = ex.Errors
				.GroupBy(x => x.PropertyName)
				.ToDictionary(x => x.Key, x => x.Select(f => f.ErrorMessage));

			await context.Response.WriteAsJsonAsync(new
			{
				ex.Message,
				Errors = errors
			});
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			_logger.LogError(ex, "{Message}", ex.Message);

			context.Response.StatusCode = StatusCodes.Status500InternalServerError;

			await context.Response.WriteAsJsonAsync(new
			{
				ex.Message
			});
		}
	}
}