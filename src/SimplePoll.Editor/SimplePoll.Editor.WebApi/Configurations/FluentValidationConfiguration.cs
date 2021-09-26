using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Application.Models.Requests;
using SimplePoll.Editor.Application.Validation;
using SimplePoll.Editor.Domain.Models;

namespace SimplePoll.Editor.Configurations
{
	internal static class FluentValidationConfiguration
	{
		internal static IServiceCollection ConfigureValidators(this IServiceCollection services)
		{
			services.AddTransient<IValidator<PollOptionDto>, PollOptionDtoValidator>();
			
			services.AddTransient<IValidator<CreatePollCommand>, CreatePollCommandValidator>();
			services.AddTransient<IValidator<UpdatePollCommand>, UpdatePollCommandValidator>();
			
			services.AddTransient<IValidator<CreatePollRequest>, CreatePollRequestValidator>();
			services.AddTransient<IValidator<UpdatePollRequest>, UpdatePollRequestValidator>();

			return services;
		}
	}
}