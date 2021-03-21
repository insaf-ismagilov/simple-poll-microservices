using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Common.Validation;
using SimplePoll.Editor.Application.Services;

namespace SimplePoll.Editor.Configurations
{
	internal static class DiConfiguration
	{
		internal static IServiceCollection ConfigureDi(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

			services.AddTransient<IPollService, PollService>();
			
			return services;
		}
	}
}