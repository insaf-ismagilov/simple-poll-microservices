using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Editor.Application.Commands;
using SimplePoll.Editor.Application.Validation;

namespace SimplePoll.Editor.Configurations
{
	internal static class FluentValidationConfiguration
	{
		internal static IServiceCollection ConfigureValidators(this IServiceCollection services)
		{
			services.AddTransient<IValidator<CreatePollCommand>, CreatePollCommandValidator>();
			services.AddTransient<IValidator<UpdatePollCommand>, UpdatePollCommandValidator>();

			return services;
		}
	}
}