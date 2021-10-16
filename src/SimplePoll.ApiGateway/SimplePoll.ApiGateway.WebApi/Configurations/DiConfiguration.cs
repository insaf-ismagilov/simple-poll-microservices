using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.ApiGateway.Application.Queries;
using SimplePoll.Common.Validation;

namespace SimplePoll.ApiGateway.WebApi.Configurations
{
	internal static class DiConfiguration
	{
		internal static IServiceCollection ConfigureDi(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetAssembly(typeof(GetPollByIdQuery)));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
			
			return services;
		}
	}
}