using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Answers.Application.Services;
using SimplePoll.Common.Validation;

namespace SimplePoll.Answers.Configurations
{
	internal static class DiConfiguration
	{
		internal static IServiceCollection ConfigureDi(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

			services.AddTransient<IPollAnswerService, PollAnswerService>();
			
			return services;
		}
	}
}