using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Common.Validation;
using SimplePoll.Editor.Application.Handlers;
using SimplePoll.Editor.Domain.Repositories;
using SimplePoll.Editor.Infrastructure.Database.Repositories;

namespace SimplePoll.Editor.Configurations
{
	internal static class DiConfiguration
	{
		internal static IServiceCollection ConfigureDi(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetAssembly(typeof(CreatePollCommandHandler)));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

			services.AddScoped<IPollRepository, PollRepository>();
			
			return services;
		}
	}
}